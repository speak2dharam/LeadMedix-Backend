using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Doctors.DTOs
{
    public class DoctorListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int TotalExperienceYears { get; set; }
        public LookupDto? CurrentHospital { get; set; }
        public string? CurrentDesignationName { get; set; }
    }
}
