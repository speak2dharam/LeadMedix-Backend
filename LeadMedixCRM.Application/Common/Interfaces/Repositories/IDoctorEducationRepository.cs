using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IDoctorEducationRepository
    {
        Task<int> AddAsync(DoctorEducation row);
        Task UpdateAsync(DoctorEducation row);
        Task SoftDeleteAsync(DoctorEducation row);
        Task<DoctorEducation?> GetByIdAsync(int id);
        Task<List<DoctorEducation>> GetByDoctorIdAsync(int doctorId);
    }
}
