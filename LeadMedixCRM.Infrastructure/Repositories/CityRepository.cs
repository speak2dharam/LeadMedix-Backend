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
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;
        public CityRepository(AppDbContext context) => _context = context;

        public Task<List<City>> GetByCountryIdAsync(int countryId)
            => _context.Cities.AsNoTracking()
                .Where(x => x.CountryId == countryId && !x.IsDeleted)
                .ToListAsync();

        public Task<City?> GetByIdAsync(int id)
            => _context.Cities.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<int> AddAsync(City entity)
        {
            _context.Cities.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        public Task<List<City>> GetByIdsAsync(List<int> ids)
        {
            return _context.Cities
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id) && !x.IsDeleted)
                .ToListAsync();
        }
        public async Task<bool> UpdateAsync(City entity)
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null) return false;
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
