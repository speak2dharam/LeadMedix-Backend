using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IDoctorRepository
    {
        Task<int> AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task SoftDeleteAsync(Doctor doctor);
        Task<Doctor?> GetByIdAsync(int id);

        Task<(List<Doctor> Data, int TotalRecords)> GetPagedAsync(int pageNumber, int pageSize);
    }
}
