using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Exceptions;
using LeadMedixCRM.Application.Features.Treatments.DTOs;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Treatments
{
    public class TreatmentCategoryService : ITreatmentCategoryService
    {
        private readonly ITreatmentCategoryRepository _repo;
        private readonly ICurrentUserService _currentUser;

        public TreatmentCategoryService(ITreatmentCategoryRepository repo, ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        public async Task<int> CreateAsync(TreatmentCategoryCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ValidationException("Name is required.");

            if (await _repo.ExistsByNameAsync(dto.Name))
                throw new ValidationException("Treatment category already exists.");

            var entity = new TreatmentCategory
            {
                Name = dto.Name.Trim(),
                SortOrder = dto.SortOrder,
                IsActive = dto.IsActive,
                CreatedBy = _currentUser.UserId
            };

            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task<string> UpdateAsync(int id, TreatmentCategoryUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Treatment category not found.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ValidationException("Name is required.");

            if (await _repo.ExistsByNameAsync(dto.Name, excludeId: id))
                throw new ValidationException("Treatment category already exists.");

            entity.Name = dto.Name.Trim();
            entity.SortOrder = dto.SortOrder;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = _currentUser.UserId;

            await _repo.UpdateAsync(entity);
            return "Treatment category updated successfully";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Treatment category not found.");

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = _currentUser.UserId;

            await _repo.UpdateAsync(entity);
            return "Treatment category deleted successfully";
        }

        public async Task<List<TreatmentCategoryListItemDto>> GetAllAsync(bool onlyActive = false)
        {
            var items = await _repo.GetAllAsync(onlyActive);

            return items.Select(x => new TreatmentCategoryListItemDto
            {
                Id = x.Id,
                Name = x.Name,
                SortOrder = x.SortOrder,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt
            }).ToList();
        }

        public async Task<PaginatedResponse<TreatmentCategoryListItemDto>> GetPagedAsync(PaginationRequest request, string? search = null)
        {
            var (items, total) = await _repo.GetPagedAsync(request, search);

            var data = items.Select(x => new TreatmentCategoryListItemDto
            {
                Id = x.Id,
                Name = x.Name,
                SortOrder = x.SortOrder,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt
            }).ToList();

            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            return new PaginatedResponse<TreatmentCategoryListItemDto>
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
