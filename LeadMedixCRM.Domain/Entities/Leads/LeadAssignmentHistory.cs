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
        public int LeadId { get; set; }

        public int? FromUserId { get; set; }
        public int ToUserId { get; set; }

        public string? Reason { get; set; }
    }
}
