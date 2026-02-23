using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Doctors.DTOs.Requests
{
    public class CreateDoctorHospitalHistoryRequest
    {
        public int HospitalId { get; set; }
        public string DesignationName { get; set; } = null!;
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Notes { get; set; }
    }

    public class UpdateDoctorHospitalHistoryRequest : CreateDoctorHospitalHistoryRequest { }
}
