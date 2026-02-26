using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Exceptions;
using LeadMedixCRM.Application.Features.Leads.LeadActivites.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.LeadActivites
{
    public class LeadActivityService : ILeadActivityService
    {
        private readonly ILeadActivityRepository _repo;
        private readonly ICurrentUserService _currentUser;
        private readonly ILeadRepository _leads; // just for validation that Lead exists

        public LeadActivityService(
            ILeadActivityRepository repo,
            ICurrentUserService currentUser,
            ILeadRepository leads)
        {
            _repo = repo;
            _currentUser = currentUser;
            _leads = leads;
        }

        public async Task<int> AddManualAsync(int leadId, LeadActivityCreateDto dto)
        {
            if (dto.ActivityType == 4)
                //throw new BadRequestException("System activity cannot be created manually.");
                throw new ValidationException("System activity cannot be created manually.");

            if (string.IsNullOrWhiteSpace(dto.Title))
                //throw new BadRequestException("Title is required.");
                throw new ValidationException("Title is required.");

            var lead = await _leads.GetByIdAsync(leadId);
            if (lead == null || lead.IsDeleted)
                throw new NotFoundException("Lead not found.");

            var activity = new LeadActivity
            {
                LeadId = leadId,
                ActivityType = dto.ActivityType,
                Title = dto.Title.Trim(),
                Summary = dto.Summary,
                NextFollowUpAt = dto.NextFollowUpAt,
                IsImportant = dto.IsImportant,
                PerformedByUserId = _currentUser.UserId,

                HospitalId = dto.HospitalId,
                QuotationId = dto.QuotationId,
                VILId = dto.VILId,
                HospitalReviewId = dto.HospitalReviewId,
                RequirementId = dto.RequirementId,
                MediaId = dto.MediaId,

                CreatedBy = _currentUser.UserId
            };

            await _repo.AddAsync(activity);

            // Lead.LastActivityAt update will be done when we implement Lead module
            return activity.Id;
        }

        public async Task<int> AddSystemAsync(int leadId, string title, string? summary = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                //throw new BadRequestException("Title is required.");
                throw new ValidationException("Title is required.");

            var lead = await _leads.GetByIdAsync(leadId);
            if (lead == null || lead.IsDeleted)
                throw new NotFoundException("Lead not found.");

            var activity = new LeadActivity
            {
                LeadId = leadId,
                ActivityType = 4, // System
                Title = title.Trim(),
                Summary = summary,
                PerformedByUserId = _currentUser.UserId, // or null if you prefer
                CreatedBy = _currentUser.UserId
            };

            await _repo.AddAsync(activity);

            return activity.Id;
        }

        public async Task<PaginatedResponse<LeadActivityListItemDto>> GetByLeadPagedAsync(
            int leadId,
            PaginationRequest request,
            int? activityType = null)
        {
            var lead = await _leads.GetByIdAsync(leadId);
            if (lead == null || lead.IsDeleted)
                throw new NotFoundException("Lead not found.");

            var (items, total) = await _repo.GetByLeadPagedAsync(leadId, request, activityType);

            var data = items.Select(x => new LeadActivityListItemDto
            {
                Id = x.Id,
                LeadId = x.LeadId,
                ActivityType = x.ActivityType,
                Title = x.Title,
                Summary = x.Summary,
                CreatedAt = x.CreatedAt,
                NextFollowUpAt = x.NextFollowUpAt,
                IsImportant = x.IsImportant,
                PerformedByUserId = x.PerformedByUserId,

                HospitalId = x.HospitalId,
                QuotationId = x.QuotationId,
                VILId = x.VILId,
                HospitalReviewId = x.HospitalReviewId,
                RequirementId = x.RequirementId,
                MediaId = x.MediaId
            }).ToList();

            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            return new PaginatedResponse<LeadActivityListItemDto>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = total,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize)
            };
        }
    }
}
