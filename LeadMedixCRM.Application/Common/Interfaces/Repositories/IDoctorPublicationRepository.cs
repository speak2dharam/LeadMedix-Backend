using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IDoctorPublicationRepository
    {
        Task<int> AddAsync(DoctorPublication row);
        Task UpdateAsync(DoctorPublication row);
        Task SoftDeleteAsync(DoctorPublication row);
        Task<DoctorPublication?> GetByIdAsync(int id);
        Task<List<DoctorPublication>> GetByDoctorIdAsync(int doctorId);
    }
}
