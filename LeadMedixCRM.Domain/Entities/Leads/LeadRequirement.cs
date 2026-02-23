using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities.Leads
{
    public class LeadRequirement : BaseEntity
    {
        public int LeadId { get; set; } // FK concept only

        public int RequirementTypeId { get; set; }     // LeadRequirementTypeMaster.Id
        public int RequirementStatusId { get; set; }   // LeadRequirementStatusMaster.Id

        public DateTime? RequestedAt { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public DateTime? VerifiedAt { get; set; }

        public string? Notes { get; set; }

        public int RequestedByUserId { get; set; }
        public int? ReceivedByUserId { get; set; }
        public int? VerifiedByUserId { get; set; }
    }
}
