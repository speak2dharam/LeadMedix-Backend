using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadAssignment.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ILeadAssignmentHistoryRepository
    {
        Task AddAsync(LeadAssignmentHistory entity);
        Task SaveChangesAsync();

        Task<PaginatedResponse<LeadAssignmentHistoryDto>>
            GetPagedByLeadIdAsync(int leadId, PaginationRequest request);
    }
}
