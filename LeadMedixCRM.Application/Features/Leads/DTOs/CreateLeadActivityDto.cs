using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.DTOs
{
    public class CreateLeadActivityDto
    {
        [Required] public int Type { get; set; } // 0 Call, 1 WhatsApp, 2 Email, 3 Note
        [Required] public string Notes { get; set; } = default!;
        public DateTime? NextFollowUpAt { get; set; }
    }
}
