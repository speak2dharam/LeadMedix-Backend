using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Features.Leads.DTOs;
using LeadMedixCRM.Domain.Entities;
using LeadMedixCRM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Infrastructure.Repositories
{
    public class LeadActivityRepository : ILeadActivityRepository
    {
        private readonly AppDbContext _db;
        public LeadActivityRepository(AppDbContext db) => _db = db;

        public Task AddAsync(LeadActivity activity, CancellationToken ct = default)
            => _db.LeadActivities.AddAsync(activity, ct).AsTask();

        public Task<List<LeadActivityResponseDto>> GetByLeadIdAsync(int leadId, CancellationToken ct = default)
            => _db.LeadActivities.AsNoTracking()
                .Where(x => x.LeadId == leadId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new LeadActivityResponseDto
                {
                    Id = x.Id,
                    LeadId = x.LeadId,
                    Type = x.Type,
                    Notes = x.Notes,
                    NextFollowUpAt = x.NextFollowUpAt,
                    CreatedByUserId = x.CreatedByUserId,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(ct);
    }
}
