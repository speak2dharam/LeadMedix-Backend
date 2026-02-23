using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Doctors.DTOs.Requests
{
    public class CreateDoctorRequest
    {
        public string Name { get; set; } = null!;
        public string? ProfileOverview { get; set; }
        public int TotalExperienceYears { get; set; }
        public int? CurrentHospitalId { get; set; }
        public string? CurrentDesignationName { get; set; }
    }

    public class UpdateDoctorRequest : CreateDoctorRequest { }
}
