using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Doctors.DTOs.Requests
{
    public class CreateDoctorMembershipRequest
    {
        public string MembershipName { get; set; } = null!;
    }

    public class CreateDoctorSpecializationRequest
    {
        public string SpecializationName { get; set; } = null!;
    }
}
