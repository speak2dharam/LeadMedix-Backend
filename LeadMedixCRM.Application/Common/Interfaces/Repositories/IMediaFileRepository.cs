using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IMediaFileRepository
    {
        Task<MediaFile?> GetPrimaryAsync(string entityType, int entityId, string mediaType);
        Task<List<MediaFile>> GetPrimaryListAsync(string entityType, List<int> entityIds, string mediaType);
        Task AddAsync(MediaFile entity);
        Task UnsetPrimaryAsync(string entityType, int entityId, string mediaType);
    }
}
