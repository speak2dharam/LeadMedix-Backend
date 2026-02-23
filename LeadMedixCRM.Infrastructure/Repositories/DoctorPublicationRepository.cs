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
    public class DoctorPublicationRepository:IDoctorPublicationRepository
    {
        private readonly AppDbContext _context;
        public DoctorPublicationRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(DoctorPublication row)
        {
            await _context.DoctorPublication.AddAsync(row);
            await _context.SaveChangesAsync();
            return row.Id;
        }

        public async Task UpdateAsync(DoctorPublication row)
        {
            _context.DoctorPublication.Update(row);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(DoctorPublication row)
        {
            row.IsDeleted = true;
            row.UpdatedAt = DateTime.UtcNow;
            _context.DoctorPublication.Update(row);
            await _context.SaveChangesAsync();
        }

        public Task<DoctorPublication?> GetByIdAsync(int id)
            => _context.DoctorPublication.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public Task<List<DoctorPublication>> GetByDoctorIdAsync(int doctorId)
            => _context.DoctorPublication
                .Where(x => x.DoctorId == doctorId && !x.IsDeleted)
                .ToListAsync();
    }
}
