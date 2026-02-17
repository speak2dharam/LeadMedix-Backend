using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class Lead : BaseEntity
    {
        public string FullName { get; set; } = default!;

        public string Phone { get; set; } = default!;
        public string PhoneNormalized { get; set; } = default!; // for duplicates

        public string? Email { get; set; }
        public string? EmailNormalized { get; set; }

        public int? CountryId { get; set; }     // FK concept only
        public int? TreatmentId { get; set; }   // FK concept only
        public int? SourceId { get; set; }      // FK concept only

        public int Temperature { get; set; }    // 0 Cold, 1 Warm, 2 Hot
        public int Status { get; set; }         // pipeline

        public int? AssignedToUserId { get; set; } // coordinator userId (FK concept)
    }
}
