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
    public class HospitalAccreditationRepository : IHospitalAccreditationRepository
    {
        private readonly AppDbContext _context;

        public HospitalAccreditationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(HospitalAccreditation entity)
        {
            await _context.HospitalAccreditations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HospitalAccreditation>> GetByHospitalIdAsync(int hospitalId)
        {
            return await _context.HospitalAccreditations
                .Where(x => x.HospitalId == hospitalId && !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<HospitalAccreditation?> GetByHospitalAndAccreditationAsync(int hospitalId, int accreditationId)
        {
            return await _context.HospitalAccreditations
                .FirstOrDefaultAsync(x => x.HospitalId == hospitalId && x.AccreditationId == accreditationId && !x.IsDeleted);
        }

        public void Update(HospitalAccreditation entity)
        {
            _context.HospitalAccreditations.Update(entity);
            _context.SaveChanges();
        }
    }
}
