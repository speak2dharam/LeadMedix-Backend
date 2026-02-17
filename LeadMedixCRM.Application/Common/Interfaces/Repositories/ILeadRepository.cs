using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.DTOs;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ILeadRepository
    {
        Task<Lead?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<Lead?> GetByPhoneNormalizedAsync(string phoneNormalized, CancellationToken ct = default);
        Task<Lead?> GetByEmailNormalizedAsync(string emailNormalized, CancellationToken ct = default);

        Task<PaginatedResponse<LeadResponseDto>> SearchAsync(LeadFilterRequest request, CancellationToken ct = default);

        Task AddAsync(Lead lead, CancellationToken ct = default);
    }
}
