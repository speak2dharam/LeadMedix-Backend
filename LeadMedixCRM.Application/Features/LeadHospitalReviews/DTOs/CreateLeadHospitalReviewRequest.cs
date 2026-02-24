using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.LeadHospitalReviews.DTOs
{
    public class CreateLeadHospitalReviewRequest
    {
        public int LeadId { get; set; }
        public int HospitalId { get; set; }
        public int ReviewStatusId { get; set; }

        public DateTime? SentAt { get; set; }
        public string? Remarks { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
