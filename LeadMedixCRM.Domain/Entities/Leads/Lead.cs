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
        public string FullName { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string PhoneNormalized { get; set; } = default!;
        public string? Email { get; set; }
        public string? EmailNormalized { get; set; }

        public int? CountryId { get; set; }
        public int? TreatmentId { get; set; }
        public int? SourceId { get; set; }

        public int Temperature { get; set; } // 0 Cold, 1 Warm, 2 Hot

        public int Status { get; set; } // LeadStatusMaster.Id (overall pipeline)

        public int? AssignedToUserId { get; set; }

        // Closure / discard metadata
        public int? DiscardReasonId { get; set; } // LeadDiscardReasonMaster.Id
        public DateTime? DiscardedAt { get; set; }

        public int? CloseReasonId { get; set; }   // LeadCloseReasonMaster.Id
        public DateTime? ClosedAt { get; set; }
    }
}
