using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadActivites.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ILeadActivityService
    {
        // Manual activity by coordinator
        Task<int> AddManualAsync(int leadId, LeadActivityCreateDto dto);

        // System activity by services (assign/status change/etc)
        Task<int> AddSystemAsync(int leadId, string title, string? summary = null);

        Task<PaginatedResponse<LeadActivityListItemDto>> GetByLeadPagedAsync(
            int leadId,
            PaginationRequest request,
            int? activityType = null
        );
    }
}
