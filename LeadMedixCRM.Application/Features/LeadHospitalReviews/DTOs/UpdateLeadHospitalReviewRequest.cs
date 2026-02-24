using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.LeadHospitalReviews.DTOs
{
    public class UpdateLeadHospitalReviewRequest
    {
        public int ReviewStatusId { get; set; }

        public DateTime? SentAt { get; set; }
        public DateTime? RespondedAt { get; set; }

        public string? Remarks { get; set; }
        public bool IsSelected { get; set; } = false;
    }

}
