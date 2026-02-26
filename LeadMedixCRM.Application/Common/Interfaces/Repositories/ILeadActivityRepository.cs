using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ILeadActivityRepository
    {
        Task AddAsync(LeadActivity activity);

        Task<(List<LeadActivity> Items, int TotalRecords)> GetByLeadPagedAsync(
            int leadId,
            PaginationRequest request,
            int? activityType = null
        );
    }
}
