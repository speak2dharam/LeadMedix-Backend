using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.DTOs
{
    public class CreateLeadDto
    {
        [Required] public string FullName { get; set; } = default!;
        [Required] public string Phone { get; set; } = default!;
        public string? Email { get; set; }

        public int? CountryId { get; set; }
        public int? TreatmentId { get; set; }
        public int? SourceId { get; set; }

        public int Temperature { get; set; } = 0; // Cold by default
        public int Status { get; set; } = 0;      // New by default
        public int? AssignedToUserId { get; set; }

        // If duplicate found, frontend can re-submit with this = true
        public bool AllowDuplicate { get; set; } = false;
    }
}
