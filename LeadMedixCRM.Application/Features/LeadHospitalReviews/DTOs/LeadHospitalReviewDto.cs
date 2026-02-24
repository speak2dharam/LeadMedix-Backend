using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.LeadHospitalReviews.DTOs
{
    public class LeadHospitalReviewDto
    {
        public int Id { get; set; }

        public int LeadId { get; set; }
        public int HospitalId { get; set; }

        public int ReviewStatusId { get; set; }
        public string? ReviewStatusName { get; set; }
        public string? ReviewStatusCode { get; set; }

        public DateTime? SentAt { get; set; }
        public DateTime? RespondedAt { get; set; }

        public string? Remarks { get; set; }
        public bool IsSelected { get; set; }

        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
