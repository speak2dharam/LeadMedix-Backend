using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IDoctorAwardRepository
    {
        Task<int> AddAsync(DoctorAward row);
        Task UpdateAsync(DoctorAward row);
        Task SoftDeleteAsync(DoctorAward row);
        Task<DoctorAward?> GetByIdAsync(int id);
        Task<List<DoctorAward>> GetByDoctorIdAsync(int doctorId);
    }
}
