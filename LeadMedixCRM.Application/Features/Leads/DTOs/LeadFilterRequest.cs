using LeadMedixCRM.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.DTOs
{
    public class LeadFilterRequest : PaginationRequest
    {
        public string? Search { get; set; } // name/phone/email
        public int? Status { get; set; }
        public int? Temperature { get; set; }
        public int? SourceId { get; set; }
        public int? CountryId { get; set; }
        public int? TreatmentId { get; set; }
        public int? AssignedToUserId { get; set; }
    }
}
