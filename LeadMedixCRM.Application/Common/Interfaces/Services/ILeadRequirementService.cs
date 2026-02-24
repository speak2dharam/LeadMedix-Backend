using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.LeadRequirements.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ILeadRequirementService
    {
        Task<LeadRequirementDto> CreateAsync(CreateLeadRequirementRequest request);
        Task<LeadRequirementDto> UpdateAsync(int id, UpdateLeadRequirementRequest request);
        Task<bool> DeleteAsync(int id);

        Task<LeadRequirementDto> GetByIdAsync(int id);

        Task<PaginatedResponse<LeadRequirementDto>> GetPagedAsync(PaginationRequest request);
        Task<PaginatedResponse<LeadRequirementDto>> GetPagedByLeadIdAsync(int leadId, PaginationRequest request);
    }
}
