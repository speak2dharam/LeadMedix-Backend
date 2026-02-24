using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Exceptions;
using LeadMedixCRM.Application.Features.LeadRequirements.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using LeadMedixCRM.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.LeadRequirements
{
    public class LeadRequirementService : ILeadRequirementService
    {
        private readonly ILeadRequirementRepository _repo;
        private readonly IMasterRepository<LeadRequirementTypeMaster> _typeRepo;
        private readonly IMasterRepository<LeadRequirementStatusMaster> _statusRepo;
        private readonly ICurrentUserService _currentUser;

        public LeadRequirementService(
            ILeadRequirementRepository repo,
            IMasterRepository<LeadRequirementTypeMaster> typeRepo,
            IMasterRepository<LeadRequirementStatusMaster> statusRepo,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _typeRepo = typeRepo;
            _statusRepo = statusRepo;
            _currentUser = currentUser;
        }

        public async Task<LeadRequirementDto> CreateAsync(CreateLeadRequirementRequest request)
        {
            if (request.LeadId <= 0) throw new ValidationException("LeadId is required.");
            if (request.RequirementTypeId <= 0) throw new ValidationException("RequirementTypeId is required.");
            if (request.RequirementStatusId <= 0) throw new ValidationException("RequirementStatusId is required.");

            var userId = _currentUser.UserId;
            if (!userId.HasValue || userId.Value <= 0)
                throw new ValidationException("Current user not found.");

            var type = await _typeRepo.GetByIdAsync(request.RequirementTypeId);
            if (type == null || !type.IsActive) throw new ValidationException("Invalid requirement type.");

            var status = await _statusRepo.GetByIdAsync(request.RequirementStatusId);
            if (status == null || !status.IsActive) throw new ValidationException("Invalid requirement status.");

            var entity = new LeadRequirement
            {
                LeadId = request.LeadId,
                RequirementTypeId = request.RequirementTypeId,
                RequirementStatusId = request.RequirementStatusId,

                RequestedAt = request.RequestedAt ?? DateTime.UtcNow,
                Notes = request.Notes,

                RequestedByUserId = userId.Value,

                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId.Value
            };

            await _repo.AddAsync(entity);
            return Map(entity, type, status);
        }

        public async Task<LeadRequirementDto> UpdateAsync(int id, UpdateLeadRequirementRequest request)
        {
            if (id <= 0) throw new ValidationException("Invalid id.");
            if (request.RequirementTypeId <= 0) throw new ValidationException("RequirementTypeId is required.");
            if (request.RequirementStatusId <= 0) throw new ValidationException("RequirementStatusId is required.");

            var userId = _currentUser.UserId;
            if (!userId.HasValue || userId.Value <= 0)
                throw new ValidationException("Current user not found.");

            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"LeadRequirement not found. Id: {id}");

            var type = await _typeRepo.GetByIdAsync(request.RequirementTypeId);
            if (type == null || !type.IsActive) throw new ValidationException("Invalid requirement type.");

            var status = await _statusRepo.GetByIdAsync(request.RequirementStatusId);
            if (status == null || !status.IsActive) throw new ValidationException("Invalid requirement status.");

            entity.RequirementTypeId = request.RequirementTypeId;
            entity.RequirementStatusId = request.RequirementStatusId;

            entity.RequestedAt = request.RequestedAt ?? entity.RequestedAt;
            entity.ReceivedAt = request.ReceivedAt ?? entity.ReceivedAt;
            entity.VerifiedAt = request.VerifiedAt ?? entity.VerifiedAt;

            entity.Notes = request.Notes;

            // Auto set "ByUserId" when timestamp is being set first time
            if (request.ReceivedAt.HasValue && entity.ReceivedByUserId == null)
                entity.ReceivedByUserId = userId.Value;

            if (request.VerifiedAt.HasValue && entity.VerifiedByUserId == null)
                entity.VerifiedByUserId = userId.Value;

            entity.UpdatedAt = DateTime.UtcNow;
            //entity.UpdatedBy = userId.Value;

            await _repo.UpdateAsync(entity);
            return Map(entity, type, status);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userId = _currentUser.UserId;
            if (!userId.HasValue || userId.Value <= 0)
                throw new ValidationException("Current user not found.");

            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"LeadRequirement not found. Id: {id}");

            entity.UpdatedAt = DateTime.UtcNow;
            //entity.UpdatedBy = userId.Value;

            await _repo.SoftDeleteAsync(entity);
            return true;
        }

        public async Task<LeadRequirementDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"LeadRequirement not found. Id: {id}");

            var type = await _typeRepo.GetByIdAsync(entity.RequirementTypeId);
            var status = await _statusRepo.GetByIdAsync(entity.RequirementStatusId);

            return Map(entity, type, status);
        }

        public async Task<PaginatedResponse<LeadRequirementDto>> GetPagedAsync(PaginationRequest request)
        {
            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize);

            var typeIds = items.Select(x => x.RequirementTypeId).Distinct().ToList();
            var statusIds = items.Select(x => x.RequirementStatusId).Distinct().ToList();

            var types = await _typeRepo.GetByIdsAsync(typeIds);
            var statuses = await _statusRepo.GetByIdsAsync(statusIds);

            var typeMap = types.ToDictionary(x => x.Id, x => x);
            var statusMap = statuses.ToDictionary(x => x.Id, x => x);

            var data = items.Select(x =>
            {
                typeMap.TryGetValue(x.RequirementTypeId, out var t);
                statusMap.TryGetValue(x.RequirementStatusId, out var s);
                return Map(x, t, s);
            }).ToList();

            return BuildPage(data, pageNumber, pageSize, total);
        }

        public async Task<PaginatedResponse<LeadRequirementDto>> GetPagedByLeadIdAsync(int leadId, PaginationRequest request)
        {
            if (leadId <= 0) throw new ValidationException("Invalid leadId.");

            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var (items, total) = await _repo.GetPagedByLeadIdAsync(leadId, pageNumber, pageSize);

            var typeIds = items.Select(x => x.RequirementTypeId).Distinct().ToList();
            var statusIds = items.Select(x => x.RequirementStatusId).Distinct().ToList();

            var types = await _typeRepo.GetByIdsAsync(typeIds);
            var statuses = await _statusRepo.GetByIdsAsync(statusIds);

            var typeMap = types.ToDictionary(x => x.Id, x => x);
            var statusMap = statuses.ToDictionary(x => x.Id, x => x);

            var data = items.Select(x =>
            {
                typeMap.TryGetValue(x.RequirementTypeId, out var t);
                statusMap.TryGetValue(x.RequirementStatusId, out var s);
                return Map(x, t, s);
            }).ToList();

            return BuildPage(data, pageNumber, pageSize, total);
        }

        private static PaginatedResponse<LeadRequirementDto> BuildPage(List<LeadRequirementDto> data, int pageNumber, int pageSize, int total)
        {
            var totalPages = (int)Math.Ceiling(total / (double)pageSize);

            return new PaginatedResponse<LeadRequirementDto>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = total,
                TotalPages = totalPages
            };
        }

        private static LeadRequirementDto Map(LeadRequirement x, LeadRequirementTypeMaster? type, LeadRequirementStatusMaster? status)
        {
            return new LeadRequirementDto
            {
                Id = x.Id,
                LeadId = x.LeadId,

                RequirementTypeId = x.RequirementTypeId,
                RequirementTypeName = type?.Name,
                RequirementTypeCode = type?.Code,

                RequirementStatusId = x.RequirementStatusId,
                RequirementStatusName = status?.Name,
                RequirementStatusCode = status?.Code,

                RequestedAt = x.RequestedAt,
                ReceivedAt = x.ReceivedAt,
                VerifiedAt = x.VerifiedAt,

                Notes = x.Notes,

                RequestedByUserId = x.RequestedByUserId,
                ReceivedByUserId = x.ReceivedByUserId,
                VerifiedByUserId = x.VerifiedByUserId,

                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                UpdatedAt = x.UpdatedAt
                //UpdatedBy = x.UpdatedBy
            };
        }
    }

}
