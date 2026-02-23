using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IAccreditationRepository
    {
        Task<List<Accreditation>> GetAllAsync();
        Task<Accreditation?> GetByIdAsync(int id);

        Task<int> AddAsync(Accreditation entity);
        Task<bool> UpdateAsync(Accreditation entity);
        Task<bool> SoftDeleteAsync(int id);
    }
}
