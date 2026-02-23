using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Doctors.DTOs.Requests
{
    public class CreateDoctorEducationRequest
    {
        public string Degree { get; set; } = null!;
        public string? Institute { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
    }

    public class UpdateDoctorEducationRequest : CreateDoctorEducationRequest { }
}
