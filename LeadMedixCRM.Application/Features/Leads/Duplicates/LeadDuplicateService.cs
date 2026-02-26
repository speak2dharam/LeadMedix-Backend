using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Exceptions;
using LeadMedixCRM.Application.Features.Leads.Duplicates.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using LeadMedixCRM.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.Duplicates
{
    public class LeadDuplicateService : ILeadDuplicateService
    {
        private readonly ILeadDuplicateRepository _repo;
        private readonly ILeadRepository _leads;
        private readonly ILeadActivityRepository _activities;
        private readonly IMasterRepository<LeadStatusMaster> _leadStatusMasters;
        private readonly ICurrentUserService _currentUser;

        public LeadDuplicateService(
            ILeadDuplicateRepository repo,
            ILeadRepository leads,
            ILeadActivityRepository activities,
            IMasterRepository<LeadStatusMaster> leadStatusMasters,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _leads = leads;
            _activities = activities;
            _leadStatusMasters = leadStatusMasters;
            _currentUser = currentUser;
        }

        public async Task<PaginatedResponse<DuplicateGroupDto>> GetDuplicateGroupsPagedAsync(PaginationRequest request)
        {
            var (items, total) = await _repo.GetDuplicateGroupsPagedAsync(request);

            var page = request.PageNumber < 1 ? 1 : request.PageNumber;
            var size = request.PageSize < 1 ? 10 : request.PageSize;
            var totalPages = (int)Math.Ceiling((double)total / size);

            return new PaginatedResponse<DuplicateGroupDto>
            {
                Data = items,
                PageNumber = page,
                PageSize = size,
                TotalRecords = total,
                TotalPages = totalPages
            };
        }

        public async Task<DuplicateGroupDetailsDto> GetDuplicateGroupDetailsAsync(int parentLeadId)
        {
            var parent = await _repo.GetLeadAsync(parentLeadId);
            if (parent == null) throw new NotFoundException("Parent lead not found.");

            var dups = await _repo.GetDuplicatesByParentIdAsync(parentLeadId);

            return new DuplicateGroupDetailsDto
            {
                Parent = Map(parent),
                Duplicates = dups.Select(Map).ToList()
            };
        }

        public async Task UnlinkDuplicateAsync(int duplicateLeadId, string? reason = null)
        {
            var dup = await _repo.GetLeadAsync(duplicateLeadId);
            if (dup == null) throw new NotFoundException("Lead not found.");
            if (!dup.IsDuplicate || dup.DuplicateOfLeadId == null)
                throw new ValidationException("This lead is not marked as duplicate.");

            var oldParent = dup.DuplicateOfLeadId.Value;

            await _repo.UnlinkDuplicateAsync(dup);

            await _activities.AddAsync(new LeadActivity
            {
                LeadId = dup.Id,
                ActivityType = 4,
                Title = "Duplicate Unlinked",
                Summary = $"Unlinked from ParentLeadId: {oldParent}. Reason: {reason}",
                PerformedByUserId = _currentUser.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            });

            await _repo.SaveChangesAsync();
        }

        public async Task MergeDuplicatesAsync(int parentLeadId, MergeDuplicatesRequest request)
        {
            if (request.DuplicateLeadIds == null || request.DuplicateLeadIds.Count == 0)
                throw new ValidationException("DuplicateLeadIds is required.");

            var parent = await _repo.GetLeadAsync(parentLeadId);
            if (parent == null) throw new NotFoundException("Parent lead not found.");

            // Load duplicates one by one (simple + safe)
            foreach (var dupId in request.DuplicateLeadIds.Distinct())
            {
                if (dupId == parentLeadId) continue;

                var dup = await _repo.GetLeadAsync(dupId);
                if (dup == null) throw new NotFoundException($"Duplicate lead not found. Id: {dupId}");

                // Must belong to same group (recommended rule)
                if (!dup.IsDuplicate || dup.DuplicateOfLeadId != parentLeadId)
                    throw new ValidationException($"Lead {dupId} is not a duplicate of parent lead {parentLeadId}.");

                // 1) Merge basic fields (only fill missing in parent)
                MergeBasicFields(parent, dup);

                // 2) Move child data (optional flags)
                if (request.MoveActivitiesToParent) await _repo.MoveLeadActivitiesAsync(dup.Id, parent.Id);
                if (request.MoveRequirementsToParent) await _repo.MoveLeadRequirementsAsync(dup.Id, parent.Id);
                if (request.MoveHospitalReviewsToParent) await _repo.MoveLeadHospitalReviewsAsync(dup.Id, parent.Id);
                if (request.MoveQuotationsToParent) await _repo.MoveLeadQuotationsAsync(dup.Id, parent.Id);
                if (request.MoveVILsToParent) await _repo.MoveLeadVILsAsync(dup.Id, parent.Id);

                // 3) Mark duplicate as merged + close it
                dup.IsMerged = true;
                dup.MergedIntoLeadId = parent.Id;
                dup.MergedAt = DateTime.UtcNow;

                dup.IsDuplicate = false;
                dup.DuplicateOfLeadId = null;

                // close duplicate lead (soft)
                dup.IsClosed = true;
                dup.ClosedAt = DateTime.UtcNow;
                dup.CloseRemarks = "Merged into another lead.";
                dup.CloseReasonId = null; // optional: you can create a CloseReasonMaster "DUPLICATE_MERGED"

                // 4) Add merge history
                await _repo.AddMergeHistoryAsync(new LeadMergeHistory
                {
                    ParentLeadId = parent.Id,
                    MergedLeadId = dup.Id,
                    Notes = request.Notes,
                    MergedByUserId = _currentUser.UserId,
                    MergedOn = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = _currentUser.UserId
                });

                // 5) Activities
                await _activities.AddAsync(new LeadActivity
                {
                    LeadId = parent.Id,
                    ActivityType = 4,
                    Title = "Lead Merged",
                    Summary = $"Merged LeadId {dup.Id} into this lead. Notes: {request.Notes}",
                    PerformedByUserId = _currentUser.UserId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = _currentUser.UserId
                });

                await _activities.AddAsync(new LeadActivity
                {
                    LeadId = dup.Id,
                    ActivityType = 4,
                    Title = "Merged Into Another Lead",
                    Summary = $"Merged into ParentLeadId {parent.Id}. Notes: {request.Notes}",
                    PerformedByUserId = _currentUser.UserId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = _currentUser.UserId
                });

                // update dup + parent
                parent.UpdatedAt = DateTime.UtcNow;
                parent.UpdatedBy = _currentUser.UserId;
                parent.LastActivityAt = DateTime.UtcNow;

                dup.UpdatedAt = DateTime.UtcNow;
                dup.UpdatedBy = _currentUser.UserId;
                dup.LastActivityAt = DateTime.UtcNow;

                await _leads.UpdateAsync(parent);
                await _leads.UpdateAsync(dup);
            }

            await _repo.SaveChangesAsync();
        }

        private void MergeBasicFields(Lead parent, Lead dup)
        {
            // Fill only if parent is empty
            parent.Phone ??= dup.Phone;
            parent.PhoneNormalized ??= dup.PhoneNormalized;
            parent.Email ??= dup.Email;
            parent.EmailNormalized ??= dup.EmailNormalized;

            parent.CountryId ??= dup.CountryId;
            parent.CityId ??= dup.CityId;

            parent.Enquiry ??= dup.Enquiry;

            parent.LeadSourceId ??= dup.LeadSourceId;
            parent.TreatmentCategoryId ??= dup.TreatmentCategoryId;
            parent.TreatmentId ??= dup.TreatmentId;

            // Notes: append instead of overwrite
            if (!string.IsNullOrWhiteSpace(dup.Notes))
            {
                if (string.IsNullOrWhiteSpace(parent.Notes))
                    parent.Notes = dup.Notes;
                else
                    parent.Notes = parent.Notes + "\n---\n" + dup.Notes;
            }
        }

        private static DuplicateLeadItemDto Map(Lead x) => new DuplicateLeadItemDto
        {
            Id = x.Id,
            FullName = x.FullName,
            Phone = x.Phone,
            Email = x.Email,
            Status = x.Status,
            IsClosed = x.IsClosed,
            IsDiscarded = x.IsDiscarded,
            CreatedAt = x.CreatedAt,
            AssignedToUserId = x.AssignedToUserId,
            Notes = x.Notes
        };
    }
}
