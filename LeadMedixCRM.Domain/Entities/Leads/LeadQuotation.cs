using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities.Leads
{
    public class LeadQuotation : BaseEntity
    {
        public int LeadId { get; set; }      // FK concept only
        public int HospitalId { get; set; }  // FK concept only

        public int QuotationStatusId { get; set; } // QuotationStatusMaster.Id

        public decimal? Amount { get; set; }
        public string? Currency { get; set; }      // "INR", "USD", etc.
        public DateTime? ValidTill { get; set; }

        public string? Inclusions { get; set; }
        public string? Exclusions { get; set; }

        public DateTime? RequestedAt { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public DateTime? SharedAt { get; set; }
    }
}
