using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadQuote.DTOs;
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
    public class LeadQuotationRepository : ILeadQuotationRepository
    {
        private readonly AppDbContext _context;

        public LeadQuotationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LeadQuotation entity)
            => await _context.Set<LeadQuotation>().AddAsync(entity);

        public async Task UpdateAsync(LeadQuotation entity)
        {
            _context.Set<LeadQuotation>().Update(entity);
            await Task.CompletedTask;
        }

        public async Task<LeadQuotation?> GetByIdAsync(int id)
            => await _context.Set<LeadQuotation>()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public async Task<PaginatedResponse<LeadQuotationDto>>
            GetPagedByLeadIdAsync(int leadId, PaginationRequest request)
        {
            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var query = _context.Set<LeadQuotation>()
                .Where(x => x.LeadId == leadId && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt);

            var totalRecords = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new LeadQuotationDto
                {
                    Id = x.Id,
                    LeadId = x.LeadId,
                    HospitalId = x.HospitalId,
                    QuotationStatusId = x.QuotationStatusId,
                    Amount = x.Amount,
                    Currency = x.Currency,
                    ValidTill = x.ValidTill,
                    Inclusions = x.Inclusions,
                    Exclusions = x.Exclusions,
                    RequestedAt = x.RequestedAt,
                    ReceivedAt = x.ReceivedAt,
                    SharedAt = x.SharedAt,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();

            return new PaginatedResponse<LeadQuotationDto>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };
        }
    }
}
