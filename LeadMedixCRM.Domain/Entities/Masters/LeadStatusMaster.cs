using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities.Masters
{
    public class LeadStatusMaster : BaseEntity
    {
        public string Code { get; set; } = default!;      // UNIQUE e.g. NEW
        public string Name { get; set; } = default!;      // "New Lead"
        public string Stage { get; set; } = default!;     // "Intake", "Review", etc.
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
