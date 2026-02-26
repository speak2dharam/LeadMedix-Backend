using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities.Leads
{
    public class LeadMergeHistory : BaseEntity
    {
        public int ParentLeadId { get; set; }
        public int MergedLeadId { get; set; }
        public string? Notes { get; set; }
        public int? MergedByUserId { get; set; }
        public DateTime MergedOn { get; set; }
    }
}
