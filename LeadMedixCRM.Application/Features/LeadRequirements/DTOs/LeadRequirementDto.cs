using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.LeadRequirements.DTOs
{
    public class LeadRequirementDto
    {
        public int Id { get; set; }

        public int LeadId { get; set; }

        public int RequirementTypeId { get; set; }
        public string? RequirementTypeName { get; set; }
        public string? RequirementTypeCode { get; set; }

        public int RequirementStatusId { get; set; }
        public string? RequirementStatusName { get; set; }
        public string? RequirementStatusCode { get; set; }

        public DateTime? RequestedAt { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public DateTime? VerifiedAt { get; set; }

        public string? Notes { get; set; }

        public int RequestedByUserId { get; set; }
        public int? ReceivedByUserId { get; set; }
        public int? VerifiedByUserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
