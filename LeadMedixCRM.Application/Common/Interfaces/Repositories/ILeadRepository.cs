using LeadMedixCRM.Application.Features.Leads.Leads.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ILeadRepository
    {
        Task<Lead?> GetByIdAsync(int id);
        Task AddAsync(Lead lead);
        Task UpdateAsync(Lead lead);

        Task<(List<Lead> Items, int TotalRecords)> GetPagedAsync(LeadFilterRequest request, int? forceAssignedToUserId = null);

        Task<bool> PhoneExistsAsync(string phoneNormalized, int? excludeLeadId = null);
        Task<bool> EmailExistsAsync(string emailNormalized, int? excludeLeadId = null);

        Task SaveChangesAsync();
        Task<Lead?> FindDuplicateAsync(string? phoneNormalized, string? emailNormalized);
    }
}
