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
    public class DoctorEducationRepository: IDoctorEducationRepository
    {
        private readonly AppDbContext _context;
        public DoctorEducationRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(DoctorEducation row)
        {
            await _context.DoctorEducation.AddAsync(row);
            await _context.SaveChangesAsync();
            return row.Id;
        }

        public async Task UpdateAsync(DoctorEducation row)
        {
            _context.DoctorEducation.Update(row);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(DoctorEducation row)
        {
            row.IsDeleted = true;
            row.UpdatedAt = DateTime.UtcNow;
            _context.DoctorEducation.Update(row);
            await _context.SaveChangesAsync();
        }

        public Task<DoctorEducation?> GetByIdAsync(int id)
            => _context.DoctorEducation.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public Task<List<DoctorEducation>> GetByDoctorIdAsync(int doctorId)
            => _context.DoctorEducation
                .Where(x => x.DoctorId == doctorId && !x.IsDeleted)
                .ToListAsync();
    }
}
