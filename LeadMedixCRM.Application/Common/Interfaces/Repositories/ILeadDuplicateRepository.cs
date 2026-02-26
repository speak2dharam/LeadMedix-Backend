using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.Duplicates.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ILeadDuplicateRepository
    {
        Task<(List<DuplicateGroupDto> Items, int TotalRecords)> GetDuplicateGroupsPagedAsync(PaginationRequest request);

        Task<Lead?> GetLeadAsync(int id);
        Task<List<Lead>> GetDuplicatesByParentIdAsync(int parentLeadId);

        Task UnlinkDuplicateAsync(Lead duplicateLead);

        Task AddMergeHistoryAsync(LeadMergeHistory history);

        // moving children (optional flags)
        Task MoveLeadActivitiesAsync(int fromLeadId, int toLeadId);
        Task MoveLeadRequirementsAsync(int fromLeadId, int toLeadId);
        Task MoveLeadHospitalReviewsAsync(int fromLeadId, int toLeadId);
        Task MoveLeadQuotationsAsync(int fromLeadId, int toLeadId);
        Task MoveLeadVILsAsync(int fromLeadId, int toLeadId);

        Task SaveChangesAsync();
    }
}
