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
    public class LeadHospitalReviewRepository : ILeadHospitalReviewRepository
    {
        private readonly AppDbContext _context;

        public LeadHospitalReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LeadHospitalReview?> GetByIdAsync(int id)
        {
            return await _context.leadHospitalReviews
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task AddAsync(LeadHospitalReview entity)
        {
            await _context.leadHospitalReviews.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LeadHospitalReview entity)
        {
            _context.leadHospitalReviews.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(LeadHospitalReview entity)
        {
            entity.IsDeleted = true;
            _context.leadHospitalReviews.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<(List<LeadHospitalReview> Items, int Total)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var q = _context.leadHospitalReviews.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id);

            var total = await q.CountAsync();
            var items = await q.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        public async Task<(List<LeadHospitalReview> Items, int Total)> GetPagedByLeadIdAsync(int leadId, int pageNumber, int pageSize)
        {
            var q = _context.leadHospitalReviews.AsNoTracking()
                .Where(x => !x.IsDeleted && x.LeadId == leadId)
                .OrderByDescending(x => x.Id);

            var total = await q.CountAsync();
            var items = await q.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
    }
}
