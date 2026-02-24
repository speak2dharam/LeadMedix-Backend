using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadVILs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ILeadVILService
    {
        Task<LeadVILDto> CreateAsync(CreateLeadVILRequest request);
        Task<LeadVILDto> UpdateAsync(int id, UpdateLeadVILRequest request);
        Task<bool> DeleteAsync(int id);

        Task<LeadVILDto> GetByIdAsync(int id);

        Task<PaginatedResponse<LeadVILDto>> GetPagedAsync(PaginationRequest request);
        Task<PaginatedResponse<LeadVILDto>> GetPagedByLeadIdAsync(int leadId, PaginationRequest request);
    }
}
