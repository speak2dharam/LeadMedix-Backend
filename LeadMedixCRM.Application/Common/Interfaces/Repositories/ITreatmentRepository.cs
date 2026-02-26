using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ITreatmentRepository
    {
        Task<Treatment?> GetByIdAsync(int id);
        Task<List<Treatment>> GetByCategoryIdAsync(int categoryId, bool onlyActive = false);
        Task<(List<Treatment> Items, int TotalRecords)> GetPagedAsync(PaginationRequest request, int? categoryId = null, string? search = null);

        Task<bool> ExistsByNameAsync(int categoryId, string name, int? excludeId = null);

        Task AddAsync(Treatment entity);
        Task UpdateAsync(Treatment entity);
    }
}
