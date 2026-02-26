using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.Leads.DTOs
{
    public class LeadListItemDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public int Status { get; set; }
        public int Temperature { get; set; }

        public int? AssignedToUserId { get; set; }
        public DateTime? LastActivityAt { get; set; }

        public bool IsDiscarded { get; set; }
        public bool IsClosed { get; set; }
    }
}
