using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class Accreditation : BaseEntity
    {
        public string Name { get; set; } = default!;   // NABH, JCI etc
        public string? Code { get; set; }              // Optional short code
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
