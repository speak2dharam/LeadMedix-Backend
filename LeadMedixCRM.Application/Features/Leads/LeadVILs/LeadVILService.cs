using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Exceptions;
using LeadMedixCRM.Application.Features.Leads.LeadVILs.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using LeadMedixCRM.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.LeadVILs
{
    public class LeadVILService : ILeadVILService
    {
        private readonly ILeadVILRepository _repo;
        private readonly IMasterRepository<VILStatusMaster> _statusRepo;
        private readonly ICurrentUserService _currentUser;

        public LeadVILService(
            ILeadVILRepository repo,
            IMasterRepository<VILStatusMaster> statusRepo,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _statusRepo = statusRepo;
            _currentUser = currentUser;
        }

        public async Task<LeadVILDto> CreateAsync(CreateLeadVILRequest request)
        {
            if (request.LeadId <= 0) throw new ValidationException("LeadId is required.");
            if (request.HospitalId <= 0) throw new ValidationException("HospitalId is required.");
            if (request.VILStatusId <= 0) throw new ValidationException("VILStatusId is required.");

            var userId = _currentUser.UserId;
            if (!userId.HasValue || userId.Value <= 0)
                throw new ValidationException("Current user not found.");

            var status = await _statusRepo.GetByIdAsync(request.VILStatusId);
            if (status == null || !status.IsActive)
                throw new ValidationException("Invalid VIL status.");

            var entity = new LeadVIL
            {
                LeadId = request.LeadId,
                HospitalId = request.HospitalId,
                VILStatusId = request.VILStatusId,

                RequestedAt = request.RequestedAt,
                IssuedAt = request.IssuedAt,

                Remarks = request.Remarks,

                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId.Value
            };

            await _repo.AddAsync(entity);
            return Map(entity, status);
        }

        public async Task<LeadVILDto> UpdateAsync(int id, UpdateLeadVILRequest request)
        {
            if (id <= 0) throw new ValidationException("Invalid id.");
            if (request.VILStatusId <= 0) throw new ValidationException("VILStatusId is required.");

            var userId = _currentUser.UserId;
            if (!userId.HasValue || userId.Value <= 0)
                throw new ValidationException("Current user not found.");

            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"LeadVIL not found. Id: {id}");

            var status = await _statusRepo.GetByIdAsync(request.VILStatusId);
            if (status == null || !status.IsActive)
                throw new ValidationException("Invalid VIL status.");

            entity.VILStatusId = request.VILStatusId;
            entity.RequestedAt = request.RequestedAt ?? entity.RequestedAt;
            entity.IssuedAt = request.IssuedAt ?? entity.IssuedAt;
            entity.Remarks = request.Remarks;

            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = userId.Value;

            await _repo.UpdateAsync(entity);
            return Map(entity, status);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userId = _currentUser.UserId;
            if (!userId.HasValue || userId.Value <= 0)
                throw new ValidationException("Current user not found.");

            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"LeadVIL not found. Id: {id}");

            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = userId.Value;

            await _repo.SoftDeleteAsync(entity);
            return true;
        }

        public async Task<LeadVILDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"LeadVIL not found. Id: {id}");

            var status = await _statusRepo.GetByIdAsync(entity.VILStatusId);
            return Map(entity, status);
        }

        public async Task<PaginatedResponse<LeadVILDto>> GetPagedAsync(PaginationRequest request)
        {
            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize);

            var statusIds = items.Select(x => x.VILStatusId).Distinct().ToList();
            var statuses = await _statusRepo.GetByIdsAsync(statusIds);
            var statusMap = statuses.ToDictionary(x => x.Id, x => x);

            var data = items.Select(x =>
            {
                statusMap.TryGetValue(x.VILStatusId, out var s);
                return Map(x, s);
            }).ToList();

            return BuildPage(data, pageNumber, pageSize, total);
        }

        public async Task<PaginatedResponse<LeadVILDto>> GetPagedByLeadIdAsync(int leadId, PaginationRequest request)
        {
            if (leadId <= 0) throw new ValidationException("Invalid leadId.");

            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var (items, total) = await _repo.GetPagedByLeadIdAsync(leadId, pageNumber, pageSize);

            var statusIds = items.Select(x => x.VILStatusId).Distinct().ToList();
            var statuses = await _statusRepo.GetByIdsAsync(statusIds);
            var statusMap = statuses.ToDictionary(x => x.Id, x => x);

            var data = items.Select(x =>
            {
                statusMap.TryGetValue(x.VILStatusId, out var s);
                return Map(x, s);
            }).ToList();

            return BuildPage(data, pageNumber, pageSize, total);
        }

        private static PaginatedResponse<LeadVILDto> BuildPage(List<LeadVILDto> data, int pageNumber, int pageSize, int total)
        {
            var totalPages = (int)Math.Ceiling(total / (double)pageSize);

            return new PaginatedResponse<LeadVILDto>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = total,
                TotalPages = totalPages
            };
        }

        private static LeadVILDto Map(LeadVIL x, VILStatusMaster? status)
        {
            return new LeadVILDto
            {
                Id = x.Id,
                LeadId = x.LeadId,
                HospitalId = x.HospitalId,

                VILStatusId = x.VILStatusId,
                VILStatusName = status?.Name,
                VILStatusCode = status?.Code,

                RequestedAt = x.RequestedAt,
                IssuedAt = x.IssuedAt,

                Remarks = x.Remarks,

                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy
            };
        }
    }
}
