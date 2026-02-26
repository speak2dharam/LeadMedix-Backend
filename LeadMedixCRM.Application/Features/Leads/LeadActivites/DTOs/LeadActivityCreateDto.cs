using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.LeadActivites.DTOs
{
    public class LeadActivityCreateDto
    {
        public int ActivityType { get; set; } // 0 Call,1 WhatsApp,2 Email,3 Note (controller should block System=4)
        public string Title { get; set; } = default!;
        public string? Summary { get; set; }

        public DateTime? NextFollowUpAt { get; set; }
        public bool IsImportant { get; set; } = false;

        // Optional linking / attachments
        public int? HospitalId { get; set; }
        public int? QuotationId { get; set; }
        public int? VILId { get; set; }
        public int? HospitalReviewId { get; set; }
        public int? RequirementId { get; set; }
        public int? MediaId { get; set; }
    }
}
