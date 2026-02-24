using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.LeadVILs.DTOs
{
    public class CreateLeadVILRequest
    {
        public int LeadId { get; set; }
        public int HospitalId { get; set; }

        public int VILStatusId { get; set; }

        public DateTime? RequestedAt { get; set; }
        public DateTime? IssuedAt { get; set; }

        public string? Remarks { get; set; }
    }
}
