using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Exceptions;
using LeadMedixCRM.Application.Features.LeadHospitalReviews.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using LeadMedixCRM.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.LeadHospitalReviews
{
    public class LeadHospitalReviewService : ILeadHospitalReviewService
    {
        private readonly ILeadHospitalReviewRepository _repo;
        private readonly IMasterRepository<HospitalReviewStatusMaster> _statusRepo;
        private readonly ICurrentUserService _currentUser;

        public LeadHospitalReviewService(
            ILeadHospitalReviewRepository repo,
            IMasterRepository<HospitalReviewStatusMaster> statusRepo,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _statusRepo = statusRepo;
            _currentUser = currentUser;
        }

        public async Task<LeadHospitalReviewDto> CreateAsync(CreateLeadHospitalReviewRequest request)
        {
            if (request.LeadId <= 0) throw new ValidationException("LeadId is required.");
            if (request.HospitalId <= 0) throw new ValidationException("HospitalId is required.");
            if (request.ReviewStatusId <= 0) throw new ValidationException("ReviewStatusId is required.");

            var userId = _currentUser.UserId;
            if (!userId.HasValue || userId.Value <= 0)
                throw new ValidationException("Current user not found.");

            var status = await _statusRepo.GetByIdAsync(request.ReviewStatusId);
            if (status == null || !status.IsActive)
                throw new ValidationException("Invalid hospital review status.");

            var entity = new LeadHospitalReview
            {
                LeadId = request.LeadId,
                HospitalId = request.HospitalId,
                ReviewStatusId = request.ReviewStatusId,

                SentAt = request.SentAt,
                RespondedAt = null,

                Remarks = request.Remarks,
                IsSelected = request.IsSelected,

                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId.Value
            };

            await _repo.AddAsync(entity);
            return Map(entity, status);
        }

        public async Task<LeadHospitalReviewDto> UpdateAsync(int id, UpdateLeadHospitalReviewRequest request)
        {
            if (id <= 0) throw new ValidationException("Invalid id.");
            if (request.ReviewStatusId <= 0) throw new ValidationException("ReviewStatusId is required.");

            var userId = _currentUser.UserId;
            if (!userId.HasValue || userId.Value <= 0)
                throw new ValidationException("Current user not found.");

            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"LeadHospitalReview not found. Id: {id}");

            var status = await _statusRepo.GetByIdAsync(request.ReviewStatusId);
            if (status == null || !status.IsActive)
                throw new ValidationException("Invalid hospital review status.");

            entity.ReviewStatusId = request.ReviewStatusId;
            entity.SentAt = request.SentAt ?? entity.SentAt;
            entity.RespondedAt = request.RespondedAt ?? entity.RespondedAt;
            entity.Remarks = request.Remarks;
            entity.IsSelected = request.IsSelected;

            entity.UpdatedAt = DateTime.UtcNow;
            //entity.UpdatedBy = userId.Value;

            await _repo.UpdateAsync(entity);
            return Map(entity, status);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userId = _currentUser.UserId;
            if (!userId.HasValue || userId.Value <= 0)
                throw new ValidationException("Current user not found.");

            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"LeadHospitalReview not found. Id: {id}");

            entity.UpdatedAt = DateTime.UtcNow;
            //entity.UpdatedBy = userId.Value;

            await _repo.SoftDeleteAsync(entity);
            return true;
        }

        public async Task<LeadHospitalReviewDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"LeadHospitalReview not found. Id: {id}");

            var status = await _statusRepo.GetByIdAsync(entity.ReviewStatusId);
            return Map(entity, status);
        }

        public async Task<PaginatedResponse<LeadHospitalReviewDto>> GetPagedAsync(PaginationRequest request)
        {
            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize);

            var statusIds = items.Select(x => x.ReviewStatusId).Distinct().ToList();
            var statuses = await _statusRepo.GetByIdsAsync(statusIds);
            var statusMap = statuses.ToDictionary(x => x.Id, x => x);

            var data = items.Select(x =>
            {
                statusMap.TryGetValue(x.ReviewStatusId, out var s);
                return Map(x, s);
            }).ToList();

            return BuildPage(data, pageNumber, pageSize, total);
        }

        public async Task<PaginatedResponse<LeadHospitalReviewDto>> GetPagedByLeadIdAsync(int leadId, PaginationRequest request)
        {
            if (leadId <= 0) throw new ValidationException("Invalid leadId.");

            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var (items, total) = await _repo.GetPagedByLeadIdAsync(leadId, pageNumber, pageSize);

            var statusIds = items.Select(x => x.ReviewStatusId).Distinct().ToList();
            var statuses = await _statusRepo.GetByIdsAsync(statusIds);
            var statusMap = statuses.ToDictionary(x => x.Id, x => x);

            var data = items.Select(x =>
            {
                statusMap.TryGetValue(x.ReviewStatusId, out var s);
                return Map(x, s);
            }).ToList();

            return BuildPage(data, pageNumber, pageSize, total);
        }

        private static PaginatedResponse<LeadHospitalReviewDto> BuildPage(List<LeadHospitalReviewDto> data, int pageNumber, int pageSize, int total)
        {
            var totalPages = (int)Math.Ceiling(total / (double)pageSize);

            return new PaginatedResponse<LeadHospitalReviewDto>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = total,
                TotalPages = totalPages
            };
        }

        private static LeadHospitalReviewDto Map(LeadHospitalReview x, HospitalReviewStatusMaster? status)
        {
            return new LeadHospitalReviewDto
            {
                Id = x.Id,
                LeadId = x.LeadId,
                HospitalId = x.HospitalId,

                ReviewStatusId = x.ReviewStatusId,
                ReviewStatusName = status?.Name,
                ReviewStatusCode = status?.Code,

                SentAt = x.SentAt,
                RespondedAt = x.RespondedAt,

                Remarks = x.Remarks,
                IsSelected = x.IsSelected,

                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                UpdatedAt = x.UpdatedAt,
                //UpdatedBy = x.UpdatedBy
            };
        }
    }
}
