using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Doctors.DTOs
{
    public class DoctorProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ProfileOverview { get; set; }
        public int TotalExperienceYears { get; set; }

        public LookupDto? CurrentHospital { get; set; }
        public string? CurrentDesignationName { get; set; }

        public List<DoctorHospitalHistoryDto> HospitalHistory { get; set; } = new();
        public List<DoctorEducationDto> Educations { get; set; } = new();
        public List<string> Memberships { get; set; } = new();
        public List<string> Specializations { get; set; } = new();
        public List<DoctorAwardDto> Awards { get; set; } = new();
        public List<DoctorPublicationDto> Publications { get; set; } = new();
        public List<DoctorFellowshipDto> Fellowships { get; set; } = new();
    }

    public class DoctorHospitalHistoryDto
    {
        public int Id { get; set; }
        public LookupDto Hospital { get; set; } = null!;
        public string DesignationName { get; set; } = null!;
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Notes { get; set; }
    }

    public class DoctorEducationDto
    {
        public int Id { get; set; }
        public string Degree { get; set; } = null!;
        public string? Institute { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
    }

    public class DoctorAwardDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int? Year { get; set; }
        public string? Issuer { get; set; }
    }

    public class DoctorPublicationDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Journal { get; set; }
        public int? Year { get; set; }
        public string? Url { get; set; }
    }

    public class DoctorFellowshipDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Organization { get; set; }
        public string? Country { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    
}
