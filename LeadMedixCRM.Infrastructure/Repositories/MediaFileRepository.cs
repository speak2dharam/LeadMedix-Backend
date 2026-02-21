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
    public class MediaFileRepository : IMediaFileRepository
    {
        private readonly AppDbContext _context;
        public MediaFileRepository(AppDbContext context) => _context = context;

        public Task<MediaFile?> GetPrimaryAsync(string entityType, int entityId, string mediaType)
            => _context.MediaFiles.AsNoTracking()
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.EntityType == entityType && x.EntityId == entityId
                                      && x.MediaType == mediaType && x.IsPrimary);

        public Task<List<MediaFile>> GetPrimaryListAsync(string entityType, List<int> entityIds, string mediaType)
            => _context.MediaFiles.AsNoTracking()
                .Where(x => !x.IsDeleted && x.EntityType == entityType && entityIds.Contains(x.EntityId)
                         && x.MediaType == mediaType && x.IsPrimary)
                .ToListAsync();

        public async Task AddAsync(MediaFile entity)
        {
            _context.MediaFiles.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UnsetPrimaryAsync(string entityType, int entityId, string mediaType)
        {
            var old = await _context.MediaFiles
                .Where(x => !x.IsDeleted && x.EntityType == entityType && x.EntityId == entityId
                         && x.MediaType == mediaType && x.IsPrimary)
                .ToListAsync();

            foreach (var x in old) x.IsPrimary = false;
            await _context.SaveChangesAsync();
        }
    }
}
