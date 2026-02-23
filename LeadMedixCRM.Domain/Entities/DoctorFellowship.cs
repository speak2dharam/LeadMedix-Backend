using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class DoctorFellowship : BaseEntity
    {
        public int DoctorId { get; set; }
        public string Title { get; set; } = null!;
        public string? Organization { get; set; }
        public string? Country { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
