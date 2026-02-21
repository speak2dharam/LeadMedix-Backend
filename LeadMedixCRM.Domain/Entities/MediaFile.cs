using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class MediaFile : BaseEntity
    {
        public string EntityType { get; set; } = default!; // "Hospital"
        public int EntityId { get; set; }
        public string MediaType { get; set; } = default!;  // "Logo"
        public string RelativePath { get; set; } = default!;

        public string FileName { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public long Size { get; set; }

        public bool IsPrimary { get; set; } = true;
    }
}
