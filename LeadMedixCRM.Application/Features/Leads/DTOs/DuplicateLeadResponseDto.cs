using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.DTOs
{
    public class DuplicateLeadResponseDto
    {
        public string Reason { get; set; } = default!;
        public LeadResponseDto ExistingLead { get; set; } = default!;
    }
}
