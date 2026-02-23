using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Doctors.DTOs.Requests
{
    public class CreateDoctorAwardRequest
    {
        public string Title { get; set; } = null!;
        public int? Year { get; set; }
        public string? Issuer { get; set; }
    }
    public class UpdateDoctorAwardRequest : CreateDoctorAwardRequest { }

    public class CreateDoctorPublicationRequest
    {
        public string Title { get; set; } = null!;
        public string? Journal { get; set; }
        public int? Year { get; set; }
        public string? Url { get; set; }
    }
    public class UpdateDoctorPublicationRequest : CreateDoctorPublicationRequest { }

    public class CreateDoctorFellowshipRequest
    {
        public string Title { get; set; } = null!;
        public string? Organization { get; set; }
        public string? Country { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    public class UpdateDoctorFellowshipRequest : CreateDoctorFellowshipRequest { }
}
