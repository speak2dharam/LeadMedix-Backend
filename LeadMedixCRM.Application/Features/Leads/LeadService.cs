using LeadMedixCRM.Application.Common.Interfaces;
using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Exceptions;
using LeadMedixCRM.Application.Features.Leads.DTOs;
using LeadMedixCRM.Domain.Entities;
using LeadMedixCRM.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads
{
    public class LeadService : ILeadService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUser;
        private readonly ILeadMastersService _leadMasterService;

        public LeadService(IUnitOfWork uow, ICurrentUserService currentUser, ILeadMastersService leadMasterService)
        {
            _uow = uow;
            _currentUser = currentUser;
            _leadMasterService = leadMasterService;
        }

        public async Task<(LeadResponseDto? Created, DuplicateLeadResponseDto? Duplicate)> CreateAsync(CreateLeadDto dto, CancellationToken ct = default)
        {
            var currentUserId = _currentUser.UserId; // ✅ always used here

            var phoneNormalized = NormalizePhone(dto.Phone);
            var emailNormalized = NormalizeEmail(dto.Email);

            var existingByPhone = await _uow.Leads.GetByPhoneNormalizedAsync(phoneNormalized, ct);
            if (existingByPhone != null && !dto.AllowDuplicate)
            {
                return (null, new DuplicateLeadResponseDto
                {
                    Reason = "Duplicate lead found by phone number.",
                    ExistingLead = Map(existingByPhone)
                });
            }

            if (!string.IsNullOrWhiteSpace(emailNormalized))
            {
                var existingByEmail = await _uow.Leads.GetByEmailNormalizedAsync(emailNormalized!, ct);
                if (existingByEmail != null && !dto.AllowDuplicate)
                {
                    return (null, new DuplicateLeadResponseDto
                    {
                        Reason = "Duplicate lead found by email address.",
                        ExistingLead = Map(existingByEmail)
                    });
                }
            }

            var lead = new Lead
            {
                FullName = dto.FullName.Trim(),
                Phone = dto.Phone.Trim(),
                PhoneNormalized = phoneNormalized,
                Email = string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim(),
                EmailNormalized = emailNormalized,

                CountryId = dto.CountryId,
                TreatmentId = dto.TreatmentId,
                SourceId = dto.SourceId,

                Temperature = dto.Temperature,
                Status = dto.Status,
                AssignedToUserId = dto.AssignedToUserId,

                CreatedBy = currentUserId
            };

            await _uow.Leads.AddAsync(lead, ct);
            await _uow.SaveAsync(ct);

            return (Map(lead), null);
        }

        public async Task<LeadResponseDto> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var lead = await _uow.Leads.GetByIdAsync(id, ct);
            if (lead == null) throw new NotFoundException("Lead not found.");
            return Map(lead);
        }

        public Task<PaginatedResponse<LeadResponseDto>> SearchAsync(LeadFilterRequest request, CancellationToken ct = default)
        {
            return _uow.Leads.SearchAsync(request, ct);
        }


        public async Task AssignAsync(int leadId, AssignLeadDto dto, CancellationToken ct = default)
        {
            var lead = await _uow.Leads.GetByIdAsync(leadId, ct);
            if (lead == null) throw new NotFoundException("Lead not found.");

            lead.AssignedToUserId = dto.AssignedToUserId;
            lead.UpdatedAt = DateTime.UtcNow;

            await _uow.SaveAsync(ct);
        }

        public async Task UpdateStatusAsync(int leadId, UpdateLeadStatusDto dto, CancellationToken ct = default)
        {
            var lead = await _uow.Leads.GetByIdAsync(leadId, ct);
            if (lead == null) throw new NotFoundException("Lead not found.");

            lead.Status = dto.Status;
            if (dto.Temperature.HasValue) lead.Temperature = dto.Temperature.Value;
            lead.UpdatedAt = DateTime.UtcNow;

            await _uow.SaveAsync(ct);
        }

        public async Task<LeadActivityResponseDto> AddActivityAsync(int leadId, CreateLeadActivityDto dto, CancellationToken ct = default)
        {
            var userId = _currentUser.UserId;
            if (userId == null) throw new ValidationException("User not logged in.");

            var lead = await _uow.Leads.GetByIdAsync(leadId, ct);
            if (lead == null) throw new NotFoundException("Lead not found.");

            var activity = new LeadActivity
            {
                LeadId = leadId,
                Type = dto.Type,
                Notes = dto.Notes.Trim(),
                NextFollowUpAt = dto.NextFollowUpAt,
                CreatedByUserId = userId.Value,
                CreatedBy = userId.Value
            };

            await _uow.LeadActivities.AddAsync(activity, ct);
            await _uow.SaveAsync(ct);

            return new LeadActivityResponseDto
            {
                Id = activity.Id,
                LeadId = activity.LeadId,
                Type = activity.Type,
                Notes = activity.Notes,
                NextFollowUpAt = activity.NextFollowUpAt,
                CreatedByUserId = activity.CreatedByUserId,
                CreatedAt = activity.CreatedAt
            };
        }

        public Task<List<LeadActivityResponseDto>> GetActivitiesAsync(int leadId, CancellationToken ct = default)
        {
            return _uow.LeadActivities.GetByLeadIdAsync(leadId, ct);
        }

        private static LeadResponseDto Map(Lead x) => new()
        {
            Id = x.Id,
            FullName = x.FullName,
            Phone = x.Phone,
            Email = x.Email,
            CountryId = x.CountryId,
            TreatmentId = x.TreatmentId,
            SourceId = x.SourceId,
            Temperature = x.Temperature,
            Status = x.Status,
            AssignedToUserId = x.AssignedToUserId,
            CreatedAt = x.CreatedAt
        };

        private static string NormalizePhone(string phone) => NormalizeDigitsOnly(phone);

        private static string NormalizeDigitsOnly(string input)
            => new string(input.Where(char.IsDigit).ToArray());

        private static string? NormalizeEmail(string? email)
            => string.IsNullOrWhiteSpace(email) ? null : email.Trim().ToLowerInvariant();
    }
}
