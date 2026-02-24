using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IMasterRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetListAsync(bool activeOnly);
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByCodeAsync(string code);

        // ✅ add this for efficient mapping
        Task<List<T>> GetByIdsAsync(IEnumerable<int> ids);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task SoftDeleteAsync(T entity);
    }
}
