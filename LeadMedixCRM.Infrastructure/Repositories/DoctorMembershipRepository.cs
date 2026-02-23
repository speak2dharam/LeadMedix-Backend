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
    public class DoctorMembershipRepository:IDoctorMembershipRepository
    {
        private readonly AppDbContext _context;
        public DoctorMembershipRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(DoctorMembership row)
        {
            await _context.DoctorMembership.AddAsync(row);
            await _context.SaveChangesAsync();
            return row.Id;
        }

        public async Task UpdateAsync(DoctorMembership row)
        {
            _context.DoctorMembership.Update(row);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(DoctorMembership row)
        {
            row.IsDeleted = true;
            row.UpdatedAt = DateTime.UtcNow;
            _context.DoctorMembership.Update(row);
            await _context.SaveChangesAsync();
        }

        public Task<DoctorMembership?> GetByIdAsync(int id)
            => _context.DoctorMembership.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public Task<List<DoctorMembership>> GetByDoctorIdAsync(int doctorId)
            => _context.DoctorMembership
                .Where(x => x.DoctorId == doctorId && !x.IsDeleted)
                .ToListAsync();
    }
}
