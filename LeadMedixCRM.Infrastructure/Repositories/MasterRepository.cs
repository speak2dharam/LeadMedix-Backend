using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Domain.Common;
using LeadMedixCRM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Infrastructure.Repositories
{
    public class MasterRepository<T> : IMasterRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _set;

        public MasterRepository(AppDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task<List<T>> GetListAsync(bool activeOnly)
        {
            var q = _set.AsNoTracking().Where(x => !x.IsDeleted);

            // If IsActive exists, filter when activeOnly=true
            if (activeOnly && typeof(T).GetProperty("IsActive") != null)
                q = q.Where(x => EF.Property<bool>(x, "IsActive") == true);

            // If SortOrder exists, order by SortOrder then Name, else Id
            if (typeof(T).GetProperty("SortOrder") != null)
            {
                q = q.OrderBy(x => EF.Property<int>(x, "SortOrder"))
                     .ThenBy(x => EF.Property<string>(x, "Name"));
            }
            else
            {
                q = q.OrderBy(x => EF.Property<int>(x, "Id"));
            }

            return await q.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
            => await _set.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id && !x.IsDeleted);

        public async Task<T?> GetByCodeAsync(string code)
            => await _set.FirstOrDefaultAsync(x =>
                EF.Property<string>(x, "Code") == code && !x.IsDeleted);

        public async Task AddAsync(T entity)
        {
            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _set.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            _set.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<T>> GetByIdsAsync(IEnumerable<int> ids)
        {
            var idList = ids.Distinct().ToList();
            if (idList.Count == 0) return new List<T>();

            return await _set.AsNoTracking()
                .Where(x => !x.IsDeleted && idList.Contains(EF.Property<int>(x, "Id")))
                .ToListAsync();
        }
    }
}
