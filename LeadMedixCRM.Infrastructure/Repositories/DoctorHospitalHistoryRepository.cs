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
    public class DoctorHospitalHistoryRepository : IDoctorHospitalHistoryRepository
    {
        private readonly AppDbContext _context;
        public DoctorHospitalHistoryRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(DoctorHospitalHistory row)
        {
            await _context.DoctorHospitalHistories.AddAsync(row);
            await _context.SaveChangesAsync();
            return row.Id;
        }

        public async Task UpdateAsync(DoctorHospitalHistory row)
        {
            _context.DoctorHospitalHistories.Update(row);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(DoctorHospitalHistory row)
        {
            row.IsDeleted = true;
            row.UpdatedAt = DateTime.UtcNow;
            _context.DoctorHospitalHistories.Update(row);
            await _context.SaveChangesAsync();
        }

        public Task<DoctorHospitalHistory?> GetByIdAsync(int id)
            => _context.DoctorHospitalHistories.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public Task<List<DoctorHospitalHistory>> GetByDoctorIdAsync(int doctorId)
            => _context.DoctorHospitalHistories
                .Where(x => x.DoctorId == doctorId && !x.IsDeleted)
                .OrderByDescending(x => x.FromDate)
                .ToListAsync();
    }
}
