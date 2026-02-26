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
        public int? CityId { get; set; }

        public int? TreatmentId { get; set; }
        public int? SourceId { get; set; }

        public int Temperature { get; set; }          // or enum LeadTemperature
        public int LeadStatusId { get; set; }         // LeadStatusMaster.Id (overall pipeline)

        public int? AssignedToUserId { get; set; }

        // Optional case summary
        public string? Diagnosis { get; set; }
        public string? Notes { get; set; }

        // Closure / discard metadata
        public bool IsDiscarded { get; set; }
        public int? DiscardReasonId { get; set; }
        public DateTime? DiscardedAt { get; set; }

        public bool IsClosed { get; set; }
        public int? CloseReasonId { get; set; }
        public DateTime? ClosedAt { get; set; }

        // Sorting / timeline
        public DateTime? LastActivityAt { get; set; }
    }
}
