using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class DoctorPublication : BaseEntity
    {
        public int DoctorId { get; set; }
        public string Title { get; set; } = null!;
        public string? Journal { get; set; }
        public int? Year { get; set; }
        public string? Url { get; set; }
    }
}
