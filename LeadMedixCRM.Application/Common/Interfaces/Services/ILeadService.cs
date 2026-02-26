using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.Leads.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ILeadService
    {
        Task<int> CreateAsync(LeadCreateDto dto);
        Task UpdateAsync(int id, LeadUpdateDto dto);

        Task<LeadListItemDto> GetByIdAsync(int id);
        Task<PaginatedResponse<LeadListItemDto>> GetPagedAsync(LeadFilterRequest request);

        Task AssignAsync(int id, LeadAssignDto dto);
        Task UpdateStatusAsync(int id, LeadStatusUpdateDto dto);

        Task DiscardAsync(int id, LeadDiscardDto dto);
        Task RestoreDiscardedAsync(int id);

        Task CloseAsync(int id, LeadCloseDto dto);
        Task ReopenAsync(int id);
    }
}
