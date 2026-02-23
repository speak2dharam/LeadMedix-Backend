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
    public class DoctorFellowshipRepository:IDoctorFellowshipRepository
    {
        private readonly AppDbContext _context;
        public DoctorFellowshipRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(DoctorFellowship row)
        {
            await _context.DoctorFellowship.AddAsync(row);
            await _context.SaveChangesAsync();
            return row.Id;
        }

        public async Task UpdateAsync(DoctorFellowship row)
        {
            _context.DoctorFellowship.Update(row);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(DoctorFellowship row)
        {
            row.IsDeleted = true;
            row.UpdatedAt = DateTime.UtcNow;
            _context.DoctorFellowship.Update(row);
            await _context.SaveChangesAsync();
        }

        public Task<DoctorFellowship?> GetByIdAsync(int id)
            => _context.DoctorFellowship.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public Task<List<DoctorFellowship>> GetByDoctorIdAsync(int doctorId)
            => _context.DoctorFellowship
                .Where(x => x.DoctorId == doctorId && !x.IsDeleted)
                .ToListAsync();
    }
}
