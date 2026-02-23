using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IDoctorHospitalHistoryRepository
    {
        Task<int> AddAsync(DoctorHospitalHistory row);
        Task UpdateAsync(DoctorHospitalHistory row);
        Task SoftDeleteAsync(DoctorHospitalHistory row);
        Task<DoctorHospitalHistory?> GetByIdAsync(int id);
        Task<List<DoctorHospitalHistory>> GetByDoctorIdAsync(int doctorId);
    }
}
