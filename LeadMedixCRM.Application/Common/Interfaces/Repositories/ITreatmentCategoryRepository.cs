using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ITreatmentCategoryRepository
    {
        Task<TreatmentCategory?> GetByIdAsync(int id);
        Task<List<TreatmentCategory>> GetAllAsync(bool onlyActive = false);
        Task<(List<TreatmentCategory> Items, int TotalRecords)> GetPagedAsync(PaginationRequest request, string? search = null);

        Task<bool> ExistsByNameAsync(string name, int? excludeId = null);

        Task AddAsync(TreatmentCategory entity);
        Task UpdateAsync(TreatmentCategory entity);
    }
}
