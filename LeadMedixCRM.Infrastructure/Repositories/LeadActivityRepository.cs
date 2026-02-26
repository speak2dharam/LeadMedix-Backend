using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Pagination;
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
    public class LeadActivityRepository : ILeadActivityRepository
    {
        private readonly AppDbContext _context;

        public LeadActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LeadActivity activity)
        {
            _context.LeadActivities.Add(activity);
            await _context.SaveChangesAsync();
        }

        public async Task<(List<LeadActivity> Items, int TotalRecords)> GetByLeadPagedAsync(
            int leadId,
            PaginationRequest request,
            int? activityType = null
        )
        {
            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            IQueryable<LeadActivity> query = _context.LeadActivities
                .AsNoTracking()
                .Where(x => x.LeadId == leadId && !x.IsDeleted);

            if (activityType.HasValue)
                query = query.Where(x => x.ActivityType == activityType.Value);

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }
    }
}
