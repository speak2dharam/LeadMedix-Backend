using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.DTOs
{
    public class LeadResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? Email { get; set; }

        public int? CountryId { get; set; }
        public int? TreatmentId { get; set; }
        public int? SourceId { get; set; }

        public int Temperature { get; set; }
        public int Status { get; set; }
        public int? AssignedToUserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
