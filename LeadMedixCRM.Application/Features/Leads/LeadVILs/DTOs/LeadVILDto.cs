using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.LeadVILs.DTOs
{
    public class LeadVILDto
    {
        public int Id { get; set; }

        public int LeadId { get; set; }
        public int HospitalId { get; set; }

        public int VILStatusId { get; set; }
        public string? VILStatusName { get; set; }
        public string? VILStatusCode { get; set; }

        public DateTime? RequestedAt { get; set; }
        public DateTime? IssuedAt { get; set; }

        public string? Remarks { get; set; }

        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
