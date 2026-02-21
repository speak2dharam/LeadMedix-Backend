using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ICityRepository
    {
        Task<List<City>> GetByCountryIdAsync(int countryId);
        Task<List<City>> GetByIdsAsync(List<int> ids);
        Task<City?> GetByIdAsync(int id);
        Task<int> AddAsync(City entity);
        Task<bool> UpdateAsync(City entity);
        Task<bool> SoftDeleteAsync(int id);
    }
}
