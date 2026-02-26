using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.Duplicates.DTOs
{
    public class DuplicateGroupDto
    {
        public int ParentLeadId { get; set; }
        public string ParentName { get; set; } = "";
        public string? ParentPhone { get; set; }
        public string? ParentEmail { get; set; }

        public int DuplicateCount { get; set; }
        public DateTime? LatestDuplicateAt { get; set; }
    }
}
