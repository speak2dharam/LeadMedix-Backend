using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities.Leads
{
    public class LeadHospitalReview : BaseEntity
    {
        public int LeadId { get; set; }      // FK concept only
        public int HospitalId { get; set; }  // FK concept only

        public int ReviewStatusId { get; set; } // HospitalReviewStatusMaster.Id

        public DateTime? SentAt { get; set; }
        public DateTime? RespondedAt { get; set; }

        public string? Remarks { get; set; }

        public bool IsSelected { get; set; } = false; // chosen hospital for next steps (optional)
    }
}
