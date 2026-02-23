using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Hospitals.DTOs
{
    public class HospitalAccreditationUpsertDto
    {
        public int AccreditationId { get; set; }
        public string? CertificateNumber { get; set; }
        public DateTime? AccreditedOn { get; set; }
        public DateTime? ValidTill { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class HospitalAccreditationViewDto
    {
        public int AccreditationId { get; set; }
        public string AccreditationName { get; set; } = default!;
        public string? AccreditationCode { get; set; }
        public string? LogoUrl { get; set; }
        public int? LogoMediaFileId { get; set; }

        public string? CertificateNumber { get; set; }
        public DateTime? AccreditedOn { get; set; }
        public DateTime? ValidTill { get; set; }
        public bool IsActive { get; set; }
    }
}
