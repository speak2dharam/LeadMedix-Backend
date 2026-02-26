using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities.Leads
{
    public class LeadActivity : BaseEntity
    {
        public int LeadId { get; set; } // FK concept only

        // 0 Call, 1 WhatsApp, 2 Email, 3 Note, 4 System
        public int ActivityType { get; set; }

        // Short timeline display
        public string Title { get; set; } = default!;
        public string? Summary { get; set; }

        // Follow-up helpers
        public DateTime? NextFollowUpAt { get; set; }
        public bool IsImportant { get; set; } = false;

        // Who performed it
        public int? PerformedByUserId { get; set; } // FK concept only

        // Optional linking (so timeline can point to module rows)
        public int? HospitalId { get; set; }
        public int? QuotationId { get; set; }
        public int? VILId { get; set; }
        public int? HospitalReviewId { get; set; }
        public int? RequirementId { get; set; }

        // Optional media attachment reference
        public int? MediaId { get; set; }
    }
}
