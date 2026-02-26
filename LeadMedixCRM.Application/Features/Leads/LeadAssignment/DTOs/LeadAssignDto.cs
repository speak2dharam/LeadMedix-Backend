using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.LeadAssignment.DTOs
{
    public class LeadAssignDto
    {
        public int AssignedToUserId { get; set; }
        public string? Reason { get; set; }
    }
}
