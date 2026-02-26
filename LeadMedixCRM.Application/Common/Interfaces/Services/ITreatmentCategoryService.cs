using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Treatments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ITreatmentCategoryService
    {
        Task<int> CreateAsync(TreatmentCategoryCreateDto dto);
        Task<string> UpdateAsync(int id, TreatmentCategoryUpdateDto dto);
        Task<string> DeleteAsync(int id);

        Task<List<TreatmentCategoryListItemDto>> GetAllAsync(bool onlyActive = false);
        Task<PaginatedResponse<TreatmentCategoryListItemDto>> GetPagedAsync(PaginationRequest request, string? search = null);
    }
}
