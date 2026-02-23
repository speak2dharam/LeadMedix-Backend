using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class HospitalAccreditation : BaseEntity
    {
        public int HospitalId { get; set; }
        public int AccreditationId { get; set; }
        public string? CertificateNumber { get; set; }
        public DateTime? AccreditedOn { get; set; }
        public DateTime? ValidTill { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
