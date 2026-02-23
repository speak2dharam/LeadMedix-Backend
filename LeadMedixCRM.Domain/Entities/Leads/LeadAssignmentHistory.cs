using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities.Leads
{
    public class LeadAssignmentHistory : BaseEntity
    {
        public int LeadId { get; set; } // FK concept only

        public int? FromUserId { get; set; } // nullable for first assignment
        public int ToUserId { get; set; }

        public string? Reason { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
        public int ChangedByUserId { get; set; }
    }
}
