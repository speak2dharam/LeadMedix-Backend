using LeadMedixCRM.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ILeadVILRepository
    {
        Task<LeadVIL?> GetByIdAsync(int id);
        Task AddAsync(LeadVIL entity);
        Task UpdateAsync(LeadVIL entity);
        Task SoftDeleteAsync(LeadVIL entity);

        Task<(List<LeadVIL> Items, int Total)> GetPagedAsync(int pageNumber, int pageSize);
        Task<(List<LeadVIL> Items, int Total)> GetPagedByLeadIdAsync(int leadId, int pageNumber, int pageSize);
    }
}
