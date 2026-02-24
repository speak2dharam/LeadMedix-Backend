using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadQuote.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ILeadQuotationService
    {
        Task<LeadQuotationDto> CreateAsync(CreateLeadQuotationDto dto);

        Task<LeadQuotationDto?> UpdateStatusAsync(UpdateQuotationStatusDto dto);

        Task<PaginatedResponse<LeadQuotationDto>>
            GetPagedByLeadIdAsync(int leadId, PaginationRequest request);
    }
}
