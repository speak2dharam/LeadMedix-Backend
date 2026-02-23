using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class DoctorEducation : BaseEntity
    {
        public int DoctorId { get; set; }
        public string Degree { get; set; } = null!;
        public string? Institute { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
    }
}
