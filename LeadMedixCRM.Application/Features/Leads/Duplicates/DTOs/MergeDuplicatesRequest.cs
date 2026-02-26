using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.Duplicates.DTOs
{
    public class MergeDuplicatesRequest
    {
        public List<int> DuplicateLeadIds { get; set; } = new();
        public string? Notes { get; set; }

        // Merge rules
        public bool MoveActivitiesToParent { get; set; } = true;
        public bool MoveRequirementsToParent { get; set; } = false;
        public bool MoveHospitalReviewsToParent { get; set; } = false;
        public bool MoveQuotationsToParent { get; set; } = false;
        public bool MoveVILsToParent { get; set; } = false;
    }
}
