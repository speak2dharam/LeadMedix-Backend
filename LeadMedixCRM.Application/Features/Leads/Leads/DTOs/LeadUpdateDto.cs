using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.Leads.DTOs
{
    public class LeadUpdateDto
    {
        public string FullName { get; set; } = default!;
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public int? CountryId { get; set; }
        public int? CityId { get; set; }

        public string? Enquiry { get; set; }
        public int Temperature { get; set; }

        public int? LeadSourceId { get; set; }
        public int? TreatmentCategoryId { get; set; }
        public int? TreatmentId { get; set; }

        public string? Notes { get; set; }
    }
}
