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
    public class TreatmentService : ITreatmentService
    {
        private readonly ITreatmentRepository _repo;
        private readonly ITreatmentCategoryRepository _categories;
        private readonly ICurrentUserService _currentUser;

        public TreatmentService(ITreatmentRepository repo, ITreatmentCategoryRepository categories, ICurrentUserService currentUser)
        {
            _repo = repo;
            _categories = categories;
            _currentUser = currentUser;
        }

        public async Task<int> CreateAsync(TreatmentCreateDto dto)
        {
            if (dto.TreatmentCategoryId <= 0)
                throw new ValidationException("TreatmentCategoryId is required.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ValidationException("Name is required.");

            var cat = await _categories.GetByIdAsync(dto.TreatmentCategoryId);
            if (cat == null)
                throw new ValidationException("Invalid TreatmentCategoryId.");

            if (await _repo.ExistsByNameAsync(dto.TreatmentCategoryId, dto.Name))
                throw new ValidationException("Treatment already exists in this category.");

            var entity = new Treatment
            {
                TreatmentCategoryId = dto.TreatmentCategoryId,
                Name = dto.Name.Trim(),
                SortOrder = dto.SortOrder,
                IsActive = dto.IsActive,
                CreatedBy = _currentUser.UserId
            };

            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task<string> UpdateAsync(int id, TreatmentUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Treatment not found.");

            if (dto.TreatmentCategoryId <= 0)
                throw new ValidationException("TreatmentCategoryId is required.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ValidationException("Name is required.");

            var cat = await _categories.GetByIdAsync(dto.TreatmentCategoryId);
            if (cat == null)
                throw new ValidationException("Invalid TreatmentCategoryId.");

            if (await _repo.ExistsByNameAsync(dto.TreatmentCategoryId, dto.Name, excludeId: id))
                throw new ValidationException("Treatment already exists in this category.");

            entity.TreatmentCategoryId = dto.TreatmentCategoryId;
            entity.Name = dto.Name.Trim();
            entity.SortOrder = dto.SortOrder;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = _currentUser.UserId;

            await _repo.UpdateAsync(entity);
            return "Treatment updated successfully";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Treatment not found.");

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = _currentUser.UserId;

            await _repo.UpdateAsync(entity);
            return "Treatment deleted successfully";
        }

        public async Task<List<TreatmentListItemDto>> GetByCategoryAsync(int categoryId, bool onlyActive = false)
        {
            if (categoryId <= 0)
                throw new ValidationException("categoryId is required.");

            var items = await _repo.GetByCategoryIdAsync(categoryId, onlyActive);

            return items.Select(x => new TreatmentListItemDto
            {
                Id = x.Id,
                TreatmentCategoryId = x.TreatmentCategoryId,
                Name = x.Name,
                SortOrder = x.SortOrder,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt
            }).ToList();
        }

        public async Task<PaginatedResponse<TreatmentListItemDto>> GetPagedAsync(PaginationRequest request, int? categoryId = null, string? search = null)
        {
            var (items, total) = await _repo.GetPagedAsync(request, categoryId, search);

            var data = items.Select(x => new TreatmentListItemDto
            {
                Id = x.Id,
                TreatmentCategoryId = x.TreatmentCategoryId,
                Name = x.Name,
                SortOrder = x.SortOrder,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt
            }).ToList();

            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            return new PaginatedResponse<TreatmentListItemDto>
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
