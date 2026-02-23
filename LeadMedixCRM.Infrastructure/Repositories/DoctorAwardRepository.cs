using LeadMedixCRM.Application.Common.Interfaces.Repositories;
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
    public class DoctorAwardRepository:IDoctorAwardRepository
    {
        private readonly AppDbContext _context;
        public DoctorAwardRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(DoctorAward row)
        {
            await _context.DoctorAward.AddAsync(row);
            await _context.SaveChangesAsync();
            return row.Id;
        }

        public async Task UpdateAsync(DoctorAward row)
        {
            _context.DoctorAward.Update(row);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(DoctorAward row)
        {
            row.IsDeleted = true;
            row.UpdatedAt = DateTime.UtcNow;
            _context.DoctorAward.Update(row);
            await _context.SaveChangesAsync();
        }

        public Task<DoctorAward?> GetByIdAsync(int id)
            => _context.DoctorAward.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public Task<List<DoctorAward>> GetByDoctorIdAsync(int doctorId)
            => _context.DoctorAward
                .Where(x => x.DoctorId == doctorId && !x.IsDeleted)
                .ToListAsync();
    }
}
