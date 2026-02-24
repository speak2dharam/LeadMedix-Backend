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
    public class LeadVILRepository : ILeadVILRepository
    {
        private readonly AppDbContext _context;

        public LeadVILRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LeadVIL?> GetByIdAsync(int id)
        {
            return await _context.leadVILs
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task AddAsync(LeadVIL entity)
        {
            await _context.leadVILs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LeadVIL entity)
        {
            _context.leadVILs.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(LeadVIL entity)
        {
            entity.IsDeleted = true;
            _context.leadVILs.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<(List<LeadVIL> Items, int Total)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var q = _context.leadVILs.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id);

            var total = await q.CountAsync();
            var items = await q.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        public async Task<(List<LeadVIL> Items, int Total)> GetPagedByLeadIdAsync(int leadId, int pageNumber, int pageSize)
        {
            var q = _context.leadVILs.AsNoTracking()
                .Where(x => !x.IsDeleted && x.LeadId == leadId)
                .OrderByDescending(x => x.Id);

            var total = await q.CountAsync();
            var items = await q.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
    }
}
