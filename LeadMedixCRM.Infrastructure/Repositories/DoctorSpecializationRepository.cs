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
    public class DoctorSpecializationRepository: IDoctorSpecializationRepository
    {
        private readonly AppDbContext _context;
        public DoctorSpecializationRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(DoctorSpecialization row)
        {
            await _context.DoctorSpecialization.AddAsync(row);
            await _context.SaveChangesAsync();
            return row.Id;
        }

        public async Task UpdateAsync(DoctorSpecialization row)
        {
            _context.DoctorSpecialization.Update(row);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(DoctorSpecialization row)
        {
            row.IsDeleted = true;
            row.UpdatedAt = DateTime.UtcNow;
            _context.DoctorSpecialization.Update(row);
            await _context.SaveChangesAsync();
        }

        public Task<DoctorSpecialization?> GetByIdAsync(int id)
            => _context.DoctorSpecialization.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public Task<List<DoctorSpecialization>> GetByDoctorIdAsync(int doctorId)
            => _context.DoctorSpecialization
                .Where(x => x.DoctorId == doctorId && !x.IsDeleted)
                .ToListAsync();
    }
}
