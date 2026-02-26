using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.LeadActivites.DTOs
{
    public class LeadActivityListItemDto
    {
        public int Id { get; set; }
        public int LeadId { get; set; }

        public int ActivityType { get; set; }
        public string Title { get; set; } = default!;
        public string? Summary { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? NextFollowUpAt { get; set; }
        public bool IsImportant { get; set; }

        public int? PerformedByUserId { get; set; }

        public int? HospitalId { get; set; }
        public int? QuotationId { get; set; }
        public int? VILId { get; set; }
        public int? HospitalReviewId { get; set; }
        public int? RequirementId { get; set; }
        public int? MediaId { get; set; }
    }
}
