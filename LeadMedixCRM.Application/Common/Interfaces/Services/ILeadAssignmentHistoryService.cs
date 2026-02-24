using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadAssignment.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ILeadAssignmentHistoryService
    {
        Task<LeadAssignmentHistoryDto> CreateAsync(CreateLeadAssignmentHistoryDto dto);

        Task<PaginatedResponse<LeadAssignmentHistoryDto>> GetPagedByLeadIdAsync(
            int leadId,
            PaginationRequest request);
    }
}
