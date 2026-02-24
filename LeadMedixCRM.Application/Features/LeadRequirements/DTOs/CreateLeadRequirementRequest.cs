using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.LeadRequirements.DTOs
{
    public class CreateLeadRequirementRequest
    {
        public int LeadId { get; set; }
        public int RequirementTypeId { get; set; }
        public int RequirementStatusId { get; set; }

        public DateTime? RequestedAt { get; set; }
        public string? Notes { get; set; }
    }
}
