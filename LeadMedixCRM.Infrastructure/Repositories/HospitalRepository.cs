using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Pagination;
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
    public class HospitalRepository : IHospitalRepository
    {
        private readonly AppDbContext _context;
        public HospitalRepository(AppDbContext context) => _context = context;

        public async Task<(List<Hospital> Items, int TotalRecords)> GetPagedAsync(PaginationRequest request)
        {
            var query = _context.Hospitals
                .AsNoTracking()
                .Where(x => !x.IsDeleted);

            //if (!string.IsNullOrWhiteSpace(request.Search))
            //{
            //    var search = request.Search.Trim();
            //    query = query.Where(x => x.Name.Contains(search) ||
            //                             (x.Code != null && x.Code.Contains(search)));
            //}

            var totalRecords = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (items, totalRecords);
        }

        public Task<Hospital?> GetByIdAsync(int id)
            => _context.Hospitals.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<int> AddAsync(Hospital entity)
        {
            _context.Hospitals.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(Hospital entity)
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await _context.Hospitals.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null) return false;
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
