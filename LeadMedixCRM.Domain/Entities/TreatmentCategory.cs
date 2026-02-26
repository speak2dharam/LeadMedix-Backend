using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class TreatmentCategory : BaseEntity
    {
        public string Name { get; set; } = default!;   // e.g. Cancer, Transplant
        public int SortOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }
}
