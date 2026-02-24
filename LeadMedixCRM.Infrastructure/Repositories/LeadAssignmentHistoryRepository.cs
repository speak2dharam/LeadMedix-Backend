using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadAssignment.DTOs;
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
    public class LeadAssignmentHistoryRepository : ILeadAssignmentHistoryRepository
    {
        private readonly AppDbContext _context;

        public LeadAssignmentHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LeadAssignmentHistory entity)
        {
            await _context.Set<LeadAssignmentHistory>().AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<LeadAssignmentHistoryDto>>
            GetPagedByLeadIdAsync(int leadId, PaginationRequest request)
        {
            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var query = _context.Set<LeadAssignmentHistory>()
                .Where(x => x.LeadId == leadId && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt);

            var totalRecords = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new LeadAssignmentHistoryDto
                {
                    Id = x.Id,
                    LeadId = x.LeadId,
                    FromUserId = x.FromUserId,
                    ToUserId = x.ToUserId,
                    Reason = x.Reason,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            return new PaginatedResponse<LeadAssignmentHistoryDto>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        }
    }
}
