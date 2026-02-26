using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.Duplicates.DTOs
{
    public class DuplicateLeadItemDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public int Status { get; set; }
        public bool IsClosed { get; set; }
        public bool IsDiscarded { get; set; }

        public DateTime CreatedAt { get; set; }
        public int? AssignedToUserId { get; set; }
        public string? Notes { get; set; }
    }
}
