using LeadMedixCRM.Application.Features.Leads.DTOs;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ILeadActivityRepository
    {
        Task AddAsync(LeadActivity activity, CancellationToken ct = default);
        Task<List<LeadActivityResponseDto>> GetByLeadIdAsync(int leadId, CancellationToken ct = default);
    }
}
