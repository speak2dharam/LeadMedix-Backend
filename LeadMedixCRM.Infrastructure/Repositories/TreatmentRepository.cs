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
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly AppDbContext _context;

        public TreatmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Treatment?> GetByIdAsync(int id)
        {
            return await _context.Treatments
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<List<Treatment>> GetByCategoryIdAsync(int categoryId, bool onlyActive = false)
        {
            var q = _context.Treatments.AsNoTracking()
                .Where(x => !x.IsDeleted && x.TreatmentCategoryId == categoryId);

            if (onlyActive)
                q = q.Where(x => x.IsActive);

            return await q.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).ToListAsync();
        }

        public async Task<(List<Treatment> Items, int TotalRecords)> GetPagedAsync(PaginationRequest request, int? categoryId = null, string? search = null)
        {
            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            IQueryable<Treatment> q = _context.Treatments.AsNoTracking()
                .Where(x => !x.IsDeleted);

            if (categoryId.HasValue)
                q = q.Where(x => x.TreatmentCategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                q = q.Where(x => x.Name.ToLower().Contains(s));
            }

            var total = await q.CountAsync();

            var items = await q
                .OrderBy(x => x.SortOrder).ThenBy(x => x.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }

        public async Task<bool> ExistsByNameAsync(int categoryId, string name, int? excludeId = null)
        {
            var nm = name.Trim().ToLower();

            var q = _context.Treatments.AsNoTracking()
                .Where(x => !x.IsDeleted
                            && x.TreatmentCategoryId == categoryId
                            && x.Name.ToLower() == nm);

            if (excludeId.HasValue)
                q = q.Where(x => x.Id != excludeId.Value);

            return await q.AnyAsync();
        }

        public async Task AddAsync(Treatment entity)
        {
            _context.Treatments.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Treatment entity)
        {
            _context.Treatments.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
