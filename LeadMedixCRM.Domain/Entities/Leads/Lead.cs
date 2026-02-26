using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities.Leads
{
    public class Lead : BaseEntity
    {
        // Patient / lead basic details
        public string FullName { get; set; } = default!;
        public string? Phone { get; set; }
        public string? PhoneNormalized { get; set; }
        public string? Email { get; set; }
        public string? EmailNormalized { get; set; }

        public int? CountryId { get; set; }  // FK concept only
        public int? CityId { get; set; }     // FK concept only

        // What is the enquiry / requirement?
        public string? Enquiry { get; set; } // e.g. "Need cost for prostate cancer surgery"

        // Lead workflow masters
        public int Status { get; set; }      // LeadStatusMaster.Id
        public int Temperature { get; set; } // 0 Cold, 1 Warm, 2 Hot

        // Assignment
        public int? AssignedToUserId { get; set; } // FK concept only

        // Source + Reporting classification
        public int? LeadSourceId { get; set; }          // LeadSource.Id
        public int? TreatmentCategoryId { get; set; }   // TreatmentCategory.Id
        public int? TreatmentId { get; set; }           // Treatment.Id

        public string? Notes { get; set; }

        // Quick reporting / listing
        public DateTime? LastActivityAt { get; set; }   // nullable

        // Close/Discard
        public bool IsDiscarded { get; set; } = false;
        public int? DiscardReasonId { get; set; }       // LeadDiscardReasonMaster.Id
        public string? DiscardRemarks { get; set; }
        public DateTime? DiscardedAt { get; set; }

        public bool IsClosed { get; set; } = false;
        public int? CloseReasonId { get; set; }         // LeadCloseReasonMaster.Id
        public string? CloseRemarks { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}
