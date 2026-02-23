using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities.Masters
{
    public class VILStatusMaster : BaseEntity
    {
        public string Code { get; set; } = default!; // UNIQUE e.g. ISSUED
        public string Name { get; set; } = default!;
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
