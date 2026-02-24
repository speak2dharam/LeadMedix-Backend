using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.LeadAssignment.DTOs
{
    public class LeadAssignmentHistoryDto
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public int? FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
