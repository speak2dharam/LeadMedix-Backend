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
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _context;
        public CountryRepository(AppDbContext context) => _context = context;

        public Task<List<Country>> GetAllAsync()
            => _context.Countries.AsNoTracking().Where(x => !x.IsDeleted).ToListAsync();

        public Task<Country?> GetByIdAsync(int id)
            => _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<int> AddAsync(Country entity)
        {
            _context.Countries.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        public Task<List<Country>> GetByIdsAsync(List<int> ids)
        {
            return _context.Countries
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id) && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Country entity)
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null) return false;
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
