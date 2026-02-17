using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = default!;   // "Admin"
        public string Code { get; set; } = default!;   // "ADMIN" (optional but useful)
    }
}
