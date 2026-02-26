using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Treatments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ITreatmentService
    {
        Task<int> CreateAsync(TreatmentCreateDto dto);
        Task<string> UpdateAsync(int id, TreatmentUpdateDto dto);
        Task<string> DeleteAsync(int id);

        Task<List<TreatmentListItemDto>> GetByCategoryAsync(int categoryId, bool onlyActive = false);
        Task<PaginatedResponse<TreatmentListItemDto>> GetPagedAsync(PaginationRequest request, int? categoryId = null, string? search = null);
    }
}
