using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Exceptions;
using LeadMedixCRM.Application.Features.Leads.Leads.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using LeadMedixCRM.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.Leads
{
    public class LeadService : ILeadService
    {
        private readonly ILeadRepository _leads;
        private readonly IMasterRepository<LeadStatusMaster> _leadStatusMasters;
        private readonly ILeadActivityRepository _activities;
        private readonly ILeadAssignmentHistoryRepository _assignHistory;
        private readonly ICurrentUserService _currentUser;

        public LeadService(
            ILeadRepository leads,
            IMasterRepository<LeadStatusMaster> leadStatusMasters,
            ILeadActivityRepository activities,
            ILeadAssignmentHistoryRepository assignHistory,
            ICurrentUserService currentUser)
        {
            _leads = leads;
            _leadStatusMasters = leadStatusMasters;
            _activities = activities;
            _assignHistory = assignHistory;
            _currentUser = currentUser;
        }

        public async Task<int> CreateAsync(LeadCreateDto dto)
        {
            var phoneNorm = NormalizePhone(dto.Phone);
            var emailNorm = NormalizeEmail(dto.Email);

            if (!string.IsNullOrWhiteSpace(phoneNorm) && await _leads.PhoneExistsAsync(phoneNorm))
                throw new ValidationException("Phone already exists for another lead.");

            if (!string.IsNullOrWhiteSpace(emailNorm) && await _leads.EmailExistsAsync(emailNorm))
                throw new ValidationException("Email already exists for another lead.");

            var newStatusId = await GetLeadStatusIdByCodeAsync("NEW");

            var lead = new Lead
            {
                FullName = dto.FullName.Trim(),
                Phone = dto.Phone?.Trim(),
                PhoneNormalized = phoneNorm,
                Email = dto.Email?.Trim(),
                EmailNormalized = emailNorm,

                CountryId = dto.CountryId,
                CityId = dto.CityId,

                Enquiry = dto.Enquiry,

                Status = newStatusId,
                Temperature = dto.Temperature,

                AssignedToUserId = dto.AssignedToUserId,

                LeadSourceId = dto.LeadSourceId,
                TreatmentCategoryId = dto.TreatmentCategoryId,
                TreatmentId = dto.TreatmentId,

                Notes = dto.Notes,
                LastActivityAt = DateTime.UtcNow,

                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            };

            await _leads.AddAsync(lead);

            // system activity
            await _activities.AddAsync(new LeadActivity
            {
                LeadId = lead.Id,
                ActivityType = 4, // System
                Title = "Lead Created",
                Summary = "Lead created in system.",
                PerformedByUserId = _currentUser.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            });

            // assignment history if assigned at create
            if (dto.AssignedToUserId.HasValue)
            {
                await _assignHistory.AddAsync(new LeadAssignmentHistory
                {
                    LeadId = lead.Id,
                    FromUserId = null,
                    ToUserId = dto.AssignedToUserId.Value,
                    Reason = "Assigned during lead creation",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = _currentUser.UserId
                });

                await _activities.AddAsync(new LeadActivity
                {
                    LeadId = lead.Id,
                    ActivityType = 4,
                    Title = "Lead Assigned",
                    Summary = $"Assigned to userId: {dto.AssignedToUserId.Value}",
                    PerformedByUserId = _currentUser.UserId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = _currentUser.UserId
                });
            }

            await _leads.SaveChangesAsync();
            return lead.Id;
        }

        public async Task UpdateAsync(int id, LeadUpdateDto dto)
        {
            var lead = await _leads.GetByIdAsync(id) ?? throw new NotFoundException("Lead not found.");

            if (lead.IsDiscarded) throw new ValidationException("Discarded lead cannot be updated. Restore it first.");
            if (lead.IsClosed) throw new ValidationException("Closed lead cannot be updated. Reopen it first.");

            var phoneNorm = NormalizePhone(dto.Phone);
            var emailNorm = NormalizeEmail(dto.Email);

            if (!string.IsNullOrWhiteSpace(phoneNorm) && await _leads.PhoneExistsAsync(phoneNorm, id))
                throw new ValidationException("Phone already exists for another lead.");

            if (!string.IsNullOrWhiteSpace(emailNorm) && await _leads.EmailExistsAsync(emailNorm, id))
                throw new ValidationException("Email already exists for another lead.");

            lead.FullName = dto.FullName.Trim();
            lead.Phone = dto.Phone?.Trim();
            lead.PhoneNormalized = phoneNorm;
            lead.Email = dto.Email?.Trim();
            lead.EmailNormalized = emailNorm;

            lead.CountryId = dto.CountryId;
            lead.CityId = dto.CityId;
            lead.Enquiry = dto.Enquiry;

            lead.Temperature = dto.Temperature;
            lead.LeadSourceId = dto.LeadSourceId;
            lead.TreatmentCategoryId = dto.TreatmentCategoryId;
            lead.TreatmentId = dto.TreatmentId;

            lead.Notes = dto.Notes;

            TouchActivity(lead);

            await _activities.AddAsync(new LeadActivity
            {
                LeadId = lead.Id,
                ActivityType = 4,
                Title = "Lead Updated",
                Summary = "Lead information updated.",
                PerformedByUserId = _currentUser.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            });

            await _leads.UpdateAsync(lead);
            await _leads.SaveChangesAsync();
        }

        public async Task<LeadListItemDto> GetByIdAsync(int id)
        {
            var lead = await _leads.GetByIdAsync(id) ?? throw new NotFoundException("Lead not found.");

            // enforce view rule for coordinator/groundstaff
            if ((IsCoordinator() || IsGroundStaff()) &&
                lead.AssignedToUserId != _currentUser.UserId)
            {
                throw new ValidationException("You can view only your assigned leads.");
            }

            return new LeadListItemDto
            {
                Id = lead.Id,
                FullName = lead.FullName,
                Phone = lead.Phone,
                Email = lead.Email,
                Status = lead.Status,
                Temperature = lead.Temperature,
                AssignedToUserId = lead.AssignedToUserId,
                LastActivityAt = lead.LastActivityAt,
                IsClosed = lead.IsClosed,
                IsDiscarded = lead.IsDiscarded
            };
        }

        public async Task<PaginatedResponse<LeadListItemDto>> GetPagedAsync(LeadFilterRequest request)
        {
            int? forceAssignedTo = null;
            if (IsCoordinator() || IsGroundStaff())
                forceAssignedTo = _currentUser.UserId;

            var (items, total) = await _leads.GetPagedAsync(request, forceAssignedTo);

            var dtos = items.Select(x => new LeadListItemDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Phone = x.Phone,
                Email = x.Email,
                Status = x.Status,
                Temperature = x.Temperature,
                AssignedToUserId = x.AssignedToUserId,
                LastActivityAt = x.LastActivityAt,
                IsClosed = x.IsClosed,
                IsDiscarded = x.IsDiscarded
            }).ToList();

            var page = request.PageNumber < 1 ? 1 : request.PageNumber;
            var size = request.PageSize < 1 ? 10 : request.PageSize;
            var totalPages = (int)Math.Ceiling((double)total / size);

            return new PaginatedResponse<LeadListItemDto>
            {
                Data = dtos,
                PageNumber = page,
                PageSize = size,
                TotalRecords = total,
                TotalPages = totalPages
            };
        }

        public async Task AssignAsync(int id, LeadAssignDto dto)
        {
            var lead = await _leads.GetByIdAsync(id) ?? throw new NotFoundException("Lead not found.");
            if (lead.IsDiscarded) throw new ValidationException("Discarded lead cannot be assigned. Restore it first.");
            if (lead.IsClosed) throw new ValidationException("Closed lead cannot be assigned. Reopen it first.");

            var from = lead.AssignedToUserId;
            lead.AssignedToUserId = dto.AssignedToUserId;

            TouchActivity(lead);

            await _assignHistory.AddAsync(new LeadAssignmentHistory
            {
                LeadId = lead.Id,
                FromUserId = from,
                ToUserId = dto.AssignedToUserId,
                Reason = dto.Remarks,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            });

            await _activities.AddAsync(new LeadActivity
            {
                LeadId = lead.Id,
                ActivityType = 4,
                Title = "Lead Assigned",
                Summary = $"Assigned from {from?.ToString() ?? "Unassigned"} to {dto.AssignedToUserId}. Reason: {dto.Remarks}",
                PerformedByUserId = _currentUser.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            });

            await _leads.UpdateAsync(lead);
            await _leads.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(int id, LeadStatusUpdateDto dto)
        {
            var lead = await _leads.GetByIdAsync(id) ?? throw new NotFoundException("Lead not found.");
            if (lead.IsDiscarded) throw new ValidationException("Discarded lead status cannot be updated. Restore it first.");
            if (lead.IsClosed) throw new ValidationException("Closed lead status cannot be updated. Reopen it first.");

            lead.Status = dto.StatusId;
            TouchActivity(lead);

            await _activities.AddAsync(new LeadActivity
            {
                LeadId = lead.Id,
                ActivityType = 4,
                Title = "Status Updated",
                Summary = dto.Remarks,
                PerformedByUserId = _currentUser.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            });

            await _leads.UpdateAsync(lead);
            await _leads.SaveChangesAsync();
        }

        public async Task DiscardAsync(int id, LeadDiscardDto dto)
        {
            var lead = await _leads.GetByIdAsync(id) ?? throw new NotFoundException("Lead not found.");

            // Coordinator can discard only own assigned lead
            if (IsCoordinator() && lead.AssignedToUserId != _currentUser.UserId)
                throw new ValidationException("Coordinator can discard only assigned leads.");

            if (lead.IsDiscarded) throw new ValidationException("Lead is already discarded.");

            lead.StatusBeforeDiscard = lead.Status;
            lead.IsDiscarded = true;
            lead.DiscardReasonId = dto.DiscardReasonId;
            lead.DiscardRemarks = dto.Remarks;
            lead.DiscardedAt = DateTime.UtcNow;

            // remove from queue
            lead.AssignedToUserId = null;

            lead.Status = await GetLeadStatusIdByCodeAsync("DISCARDED");

            TouchActivity(lead);

            await _activities.AddAsync(new LeadActivity
            {
                LeadId = lead.Id,
                ActivityType = 4,
                Title = "Lead Discarded",
                Summary = dto.Remarks,
                PerformedByUserId = _currentUser.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            });

            await _leads.UpdateAsync(lead);
            await _leads.SaveChangesAsync();
        }

        public async Task RestoreDiscardedAsync(int id)
        {
            var lead = await _leads.GetByIdAsync(id) ?? throw new NotFoundException("Lead not found.");
            if (!lead.IsDiscarded) throw new ValidationException("Lead is not discarded.");

            lead.IsDiscarded = false;
            lead.DiscardReasonId = null;
            lead.DiscardRemarks = null;
            lead.DiscardedAt = null;

            lead.Status = lead.StatusBeforeDiscard ?? await GetLeadStatusIdByCodeAsync("NEW");
            lead.StatusBeforeDiscard = null;

            TouchActivity(lead);

            await _activities.AddAsync(new LeadActivity
            {
                LeadId = lead.Id,
                ActivityType = 4,
                Title = "Discard Restored",
                Summary = "Lead restored from discarded.",
                PerformedByUserId = _currentUser.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            });

            await _leads.UpdateAsync(lead);
            await _leads.SaveChangesAsync();
        }

        public async Task CloseAsync(int id, LeadCloseDto dto)
        {
            var lead = await _leads.GetByIdAsync(id) ?? throw new NotFoundException("Lead not found.");
            if (lead.IsClosed) throw new ValidationException("Lead is already closed.");

            // Coordinator can close only own assigned lead
            if (IsCoordinator() && lead.AssignedToUserId != _currentUser.UserId)
                throw new ValidationException("Coordinator can close only assigned leads.");

            lead.StatusBeforeClose = lead.Status;

            lead.IsClosed = true;
            lead.CloseReasonId = dto.CloseReasonId;
            lead.CloseRemarks = dto.Remarks;
            lead.ClosedAt = DateTime.UtcNow;

            // You can change code if you want CLOSED instead of CLOSED_NO_RESPONSE
            lead.Status = await GetLeadStatusIdByCodeAsync("CLOSED_NO_RESPONSE");

            TouchActivity(lead);

            await _activities.AddAsync(new LeadActivity
            {
                LeadId = lead.Id,
                ActivityType = 4,
                Title = "Lead Closed",
                Summary = dto.Remarks,
                PerformedByUserId = _currentUser.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            });

            await _leads.UpdateAsync(lead);
            await _leads.SaveChangesAsync();
        }

        public async Task ReopenAsync(int id)
        {
            var lead = await _leads.GetByIdAsync(id) ?? throw new NotFoundException("Lead not found.");
            if (!lead.IsClosed) throw new ValidationException("Lead is not closed.");

            lead.IsClosed = false;
            lead.CloseReasonId = null;
            lead.CloseRemarks = null;
            lead.ClosedAt = null;

            lead.Status = lead.StatusBeforeClose ?? await GetLeadStatusIdByCodeAsync("NEW");
            lead.StatusBeforeClose = null;

            TouchActivity(lead);

            await _activities.AddAsync(new LeadActivity
            {
                LeadId = lead.Id,
                ActivityType = 4,
                Title = "Lead Reopened",
                Summary = "Lead reopened.",
                PerformedByUserId = _currentUser.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            });

            await _leads.UpdateAsync(lead);
            await _leads.SaveChangesAsync();
        }

        // ---------------- helpers ----------------
        private bool HasRole(string roleCode) =>
            _currentUser.Roles.Any(r =>
                r.Equals(roleCode, StringComparison.OrdinalIgnoreCase));

        private bool IsCoordinator() => HasRole("COORDINATOR");

        private bool IsGroundStaff() => HasRole("GROUNDSTAFF");

        private bool IsManager() => HasRole("MANAGER");

        private bool IsAdmin() => HasRole("ADMIN");
        private void TouchActivity(Lead lead)
        {
            lead.LastActivityAt = DateTime.UtcNow;
            lead.UpdatedAt = DateTime.UtcNow;
            lead.UpdatedBy = _currentUser.UserId;
        }

        private async Task<int> GetLeadStatusIdByCodeAsync(string code)
        {
            var row = await _leadStatusMasters.GetByCodeAsync(code);
            if (row == null)
                throw new ValidationException($"LeadStatusMaster code not found: {code}");

            if (!row.IsActive)
                throw new ValidationException($"LeadStatusMaster '{code}' is inactive.");

            return row.Id;
        }

        private static string? NormalizeEmail(string? email)
            => string.IsNullOrWhiteSpace(email) ? null : email.Trim().ToLower();

        private static string? NormalizePhone(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return null;
            return Regex.Replace(phone, @"\D", "");
        }
    }
}
