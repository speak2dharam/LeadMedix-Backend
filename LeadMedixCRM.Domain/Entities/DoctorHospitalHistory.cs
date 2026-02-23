using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class DoctorHospitalHistory : BaseEntity
    {
        public int DoctorId { get; set; }             // logical
        public int HospitalId { get; set; }           // logical
        public string DesignationName { get; set; } = null!; // stored directly
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Notes { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
