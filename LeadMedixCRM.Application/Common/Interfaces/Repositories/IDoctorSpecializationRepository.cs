using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IDoctorSpecializationRepository
    {
        Task<int> AddAsync(DoctorSpecialization row);
        Task UpdateAsync(DoctorSpecialization row);
        Task SoftDeleteAsync(DoctorSpecialization row);
        Task<DoctorSpecialization?> GetByIdAsync(int id);
        Task<List<DoctorSpecialization>> GetByDoctorIdAsync(int doctorId);
    }
}
