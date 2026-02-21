using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class City : BaseEntity
    {
        public int CountryId { get; set; }
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; } = true;
    }
}
