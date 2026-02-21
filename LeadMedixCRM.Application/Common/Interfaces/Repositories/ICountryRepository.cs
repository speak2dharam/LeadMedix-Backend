using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAllAsync();
        Task<List<Country>> GetByIdsAsync(List<int> ids);
        Task<Country?> GetByIdAsync(int id);
        Task<int> AddAsync(Country entity);
        Task<bool> UpdateAsync(Country entity);
        Task<bool> SoftDeleteAsync(int id);
    }
}
