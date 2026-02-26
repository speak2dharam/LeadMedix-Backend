using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.Duplicates.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ILeadDuplicateService
    {
        Task<PaginatedResponse<DuplicateGroupDto>> GetDuplicateGroupsPagedAsync(PaginationRequest request);
        Task<DuplicateGroupDetailsDto> GetDuplicateGroupDetailsAsync(int parentLeadId);

        Task UnlinkDuplicateAsync(int duplicateLeadId, string? reason = null);

        Task MergeDuplicatesAsync(int parentLeadId, MergeDuplicatesRequest request);
    }
}
