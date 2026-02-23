using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities.Masters
{
    public class LeadCloseReasonMaster : BaseEntity
    {
        public string Code { get; set; } = default!;  // UNIQUE e.g. NO_RESPONSE
        public string Name { get; set; } = default!;
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
