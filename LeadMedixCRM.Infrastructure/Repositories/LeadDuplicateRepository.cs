using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.Duplicates.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using LeadMedixCRM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Infrastructure.Repositories
{
    public class LeadDuplicateRepository : ILeadDuplicateRepository
    {
        private readonly AppDbContext _context;
        public LeadDuplicateRepository(AppDbContext context) => _context = context;

        public Task<Lead?> GetLeadAsync(int id)
            => _context.Leads.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<(List<DuplicateGroupDto> Items, int TotalRecords)> GetDuplicateGroupsPagedAsync(PaginationRequest request)
        {
            var q = _context.Leads.AsNoTracking()
                .Where(x => x.IsDuplicate && x.DuplicateOfLeadId != null && !x.IsDeleted);

            // group
            var grouped = q.GroupBy(x => x.DuplicateOfLeadId!.Value)
                .Select(g => new
                {
                    ParentLeadId = g.Key,
                    DuplicateCount = g.Count(),
                    LatestDuplicateAt = g.Max(x => x.CreatedAt)
                });

            var total = await grouped.CountAsync();

            var page = request.PageNumber < 1 ? 1 : request.PageNumber;
            var size = request.PageSize < 1 ? 10 : request.PageSize;

            var groups = await grouped
                .OrderByDescending(x => x.LatestDuplicateAt)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            var parentIds = groups.Select(x => x.ParentLeadId).ToList();

            var parents = await _context.Leads.AsNoTracking()
                .Where(x => parentIds.Contains(x.Id))
                .Select(x => new { x.Id, x.FullName, x.Phone, x.Email })
                .ToListAsync();

            var parentMap = parents.ToDictionary(x => x.Id);

            var result = groups.Select(x =>
            {
                parentMap.TryGetValue(x.ParentLeadId, out var p);
                return new DuplicateGroupDto
                {
                    ParentLeadId = x.ParentLeadId,
                    ParentName = p?.FullName ?? "(Parent not found)",
                    ParentPhone = p?.Phone,
                    ParentEmail = p?.Email,
                    DuplicateCount = x.DuplicateCount,
                    LatestDuplicateAt = x.LatestDuplicateAt
                };
            }).ToList();

            return (result, total);
        }

        public async Task<List<Lead>> GetDuplicatesByParentIdAsync(int parentLeadId)
        {
            return await _context.Leads.AsNoTracking()
                .Where(x => x.IsDuplicate && x.DuplicateOfLeadId == parentLeadId && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public Task UnlinkDuplicateAsync(Lead duplicateLead)
        {
            duplicateLead.IsDuplicate = false;
            duplicateLead.DuplicateOfLeadId = null;
            _context.Leads.Update(duplicateLead);
            return Task.CompletedTask;
        }

        public async Task AddMergeHistoryAsync(LeadMergeHistory history)
            => await _context.leadMergeHistories.AddAsync(history);

        public async Task MoveLeadActivitiesAsync(int fromLeadId, int toLeadId)
        {
            await _context.LeadActivities
                .Where(x => x.LeadId == fromLeadId && !x.IsDeleted)
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.LeadId, toLeadId));
        }

        public async Task MoveLeadRequirementsAsync(int fromLeadId, int toLeadId)
        {
            await _context.leadRequirements
                .Where(x => x.LeadId == fromLeadId && !x.IsDeleted)
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.LeadId, toLeadId));
        }

        public async Task MoveLeadHospitalReviewsAsync(int fromLeadId, int toLeadId)
        {
            await _context.leadHospitalReviews
                .Where(x => x.LeadId == fromLeadId && !x.IsDeleted)
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.LeadId, toLeadId));
        }

        public async Task MoveLeadQuotationsAsync(int fromLeadId, int toLeadId)
        {
            await _context.leadQuotations
                .Where(x => x.LeadId == fromLeadId && !x.IsDeleted)
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.LeadId, toLeadId));
        }

        public async Task MoveLeadVILsAsync(int fromLeadId, int toLeadId)
        {
            await _context.leadVILs
                .Where(x => x.LeadId == fromLeadId && !x.IsDeleted)
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.LeadId, toLeadId));
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
