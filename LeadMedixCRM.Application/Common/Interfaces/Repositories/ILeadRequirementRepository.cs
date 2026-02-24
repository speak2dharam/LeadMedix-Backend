using LeadMedixCRM.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ILeadRequirementRepository
    {
        Task<LeadRequirement?> GetByIdAsync(int id);
        Task AddAsync(LeadRequirement entity);
        Task UpdateAsync(LeadRequirement entity);
        Task SoftDeleteAsync(LeadRequirement entity);

        Task<(List<LeadRequirement> Items, int Total)> GetPagedAsync(int pageNumber, int pageSize);
        Task<(List<LeadRequirement> Items, int Total)> GetPagedByLeadIdAsync(int leadId, int pageNumber, int pageSize);
    }
}
