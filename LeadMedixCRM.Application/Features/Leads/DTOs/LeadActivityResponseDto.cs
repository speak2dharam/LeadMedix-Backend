using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.DTOs
{
    public class LeadActivityResponseDto
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public int Type { get; set; }
        public string Notes { get; set; } = default!;
        public DateTime? NextFollowUpAt { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
