using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IDoctorFellowshipRepository
    {
        Task<int> AddAsync(DoctorFellowship row);
        Task UpdateAsync(DoctorFellowship row);
        Task SoftDeleteAsync(DoctorFellowship row);
        Task<DoctorFellowship?> GetByIdAsync(int id);
        Task<List<DoctorFellowship>> GetByDoctorIdAsync(int doctorId);
    }
}
