using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadQuote.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ILeadQuotationRepository
    {
        Task AddAsync(LeadQuotation entity);
        Task UpdateAsync(LeadQuotation entity);
        Task<LeadQuotation?> GetByIdAsync(int id);
        Task SaveChangesAsync();

        Task<PaginatedResponse<LeadQuotationDto>>
            GetPagedByLeadIdAsync(int leadId, PaginationRequest request);
    }
}
