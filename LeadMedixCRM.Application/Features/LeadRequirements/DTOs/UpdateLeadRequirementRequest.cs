using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.LeadRequirements.DTOs
{
    public class UpdateLeadRequirementRequest
    {
        public int RequirementTypeId { get; set; }
        public int RequirementStatusId { get; set; }

        public DateTime? RequestedAt { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public DateTime? VerifiedAt { get; set; }

        public string? Notes { get; set; }
    }   
}
