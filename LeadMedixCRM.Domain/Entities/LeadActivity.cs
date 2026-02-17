using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class LeadActivity : BaseEntity
    {
        public int LeadId { get; set; }          // FK concept only
        public int Type { get; set; }            // 0 Call, 1 WhatsApp, 2 Email, 3 Note
        public string Notes { get; set; } = default!;
        public DateTime? NextFollowUpAt { get; set; }

        public int CreatedByUserId { get; set; }
    }
}
