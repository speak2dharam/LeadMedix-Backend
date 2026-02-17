using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Iso2 { get; set; } = default!;
        public string PhoneCode { get; set; } = default!;
    }
}
