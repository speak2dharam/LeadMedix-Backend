using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ILeadService
    {
        Task<(LeadResponseDto? Created, DuplicateLeadResponseDto? Duplicate)> CreateAsync(CreateLeadDto dto, CancellationToken ct = default);
        Task<LeadResponseDto> GetByIdAsync(int id, CancellationToken ct = default);
        Task<PaginatedResponse<LeadResponseDto>> SearchAsync(LeadFilterRequest request, CancellationToken ct = default);

        Task AssignAsync(int leadId, AssignLeadDto dto, CancellationToken ct = default);
        Task UpdateStatusAsync(int leadId, UpdateLeadStatusDto dto, CancellationToken ct = default);

        Task<LeadActivityResponseDto> AddActivityAsync(int leadId, CreateLeadActivityDto dto, CancellationToken ct = default);
        Task<List<LeadActivityResponseDto>> GetActivitiesAsync(int leadId, CancellationToken ct = default);
    }
}
