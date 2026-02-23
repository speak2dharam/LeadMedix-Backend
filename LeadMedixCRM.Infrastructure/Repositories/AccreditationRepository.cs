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
    public class AccreditationRepository : IAccreditationRepository
    {
        private readonly AppDbContext _context;

        public AccreditationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Accreditation>> GetAllAsync()
        {
            return await _context.Accreditations
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Accreditation?> GetByIdAsync(int id)
        {
            return await _context.Accreditations
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<int> AddAsync(Accreditation entity)
        {
            await _context.Accreditations.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(Accreditation entity)
        {
            _context.Accreditations.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await _context.Accreditations
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (entity == null) return false;

            entity.IsDeleted = true;
            _context.Accreditations.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
