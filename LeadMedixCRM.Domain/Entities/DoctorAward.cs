using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class DoctorAward : BaseEntity
    {
        public int DoctorId { get; set; }
        public string Title { get; set; } = null!;
        public int? Year { get; set; }
        public string? Issuer { get; set; }
    }
}
