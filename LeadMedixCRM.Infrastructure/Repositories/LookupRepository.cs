using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Infrastructure.Repositories
{
    public class LookupRepository : ILookupRepository
    {
        private readonly AppDbContext _context;
        public LookupRepository(AppDbContext context) => _context = context;

        public async Task<Dictionary<int, string>> GetHospitalNamesByIdsAsync(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return new Dictionary<int, string>();

            var rows = await _context.Hospitals
                .Where(h => ids.Contains(h.Id) && !h.IsDeleted)
                .Select(h => new { h.Id, h.Name })
                .ToListAsync();

            return rows.ToDictionary(x => x.Id, x => x.Name);
        }

        public async Task<string?> GetHospitalNameByIdAsync(int id)
        {
            return await _context.Hospitals
                .Where(h => h.Id == id && !h.IsDeleted)
                .Select(h => h.Name)
                .FirstOrDefaultAsync();
        }
    }
}
