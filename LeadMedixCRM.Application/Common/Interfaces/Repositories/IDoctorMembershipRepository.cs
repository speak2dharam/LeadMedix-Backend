using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IDoctorMembershipRepository
    {
        Task<int> AddAsync(DoctorMembership row);
        Task UpdateAsync(DoctorMembership row);
        Task SoftDeleteAsync(DoctorMembership row);
        Task<DoctorMembership?> GetByIdAsync(int id);
        Task<List<DoctorMembership>> GetByDoctorIdAsync(int doctorId);
    }
}
