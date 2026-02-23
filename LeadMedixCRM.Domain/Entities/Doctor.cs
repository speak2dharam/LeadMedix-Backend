using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? ProfileOverview { get; set; }
        public int TotalExperienceYears { get; set; }

        // Current info (no master table for designation)
        public int? CurrentHospitalId { get; set; }          // logical Hospital
        public string? CurrentDesignationName { get; set; }  // stored directly
        public int? UpdatedBy { get; set; }
    }
}
