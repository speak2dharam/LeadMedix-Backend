using LeadMedixCRM.Application.Common.Interfaces.Repositories;
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
    public class LeadRequirementRepository : ILeadRequirementRepository
    {
        private readonly AppDbContext _context;

        public LeadRequirementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LeadRequirement?> GetByIdAsync(int id)
        {
            return await _context.leadRequirements
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task AddAsync(LeadRequirement entity)
        {
            await _context.leadRequirements.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LeadRequirement entity)
        {
            _context.leadRequirements.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(LeadRequirement entity)
        {
            entity.IsDeleted = true;
            _context.leadRequirements.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<(List<LeadRequirement> Items, int Total)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var q = _context.leadRequirements.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id);

            var total = await q.CountAsync();
            var items = await q.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        public async Task<(List<LeadRequirement> Items, int Total)> GetPagedByLeadIdAsync(int leadId, int pageNumber, int pageSize)
        {
            var q = _context.leadRequirements.AsNoTracking()
                .Where(x => !x.IsDeleted && x.LeadId == leadId)
                .OrderByDescending(x => x.Id);

            var total = await q.CountAsync();
            var items = await q.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
    }
}
