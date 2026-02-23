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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;
        public DoctorRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return doctor.Id;
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Doctor doctor)
        {
            doctor.IsDeleted = true;
            doctor.UpdatedAt = DateTime.UtcNow;
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public Task<Doctor?> GetByIdAsync(int id)
            => _context.Doctors.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public async Task<(List<Doctor> Data, int TotalRecords)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var q = _context.Doctors.Where(x => !x.IsDeleted).OrderByDescending(x => x.Id);
            var total = await q.CountAsync();
            var data = await q.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (data, total);
        }
    }
}
