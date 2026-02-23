using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities.Leads
{
    public class LeadVIL : BaseEntity
    {
        public int LeadId { get; set; }      // FK concept only
        public int HospitalId { get; set; }  // FK concept only

        public int VILStatusId { get; set; } // VILStatusMaster.Id

        public DateTime? RequestedAt { get; set; }
        public DateTime? IssuedAt { get; set; }

        public string? Remarks { get; set; }
    }
}
