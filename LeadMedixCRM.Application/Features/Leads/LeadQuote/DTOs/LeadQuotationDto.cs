using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.LeadQuote.DTOs
{
    public class LeadQuotationDto
    {
        public int Id { get; set; }

        public int LeadId { get; set; }
        public int HospitalId { get; set; }

        public int QuotationStatusId { get; set; }

        public decimal? Amount { get; set; }
        public string? Currency { get; set; }
        public DateTime? ValidTill { get; set; }

        public string? Inclusions { get; set; }
        public string? Exclusions { get; set; }

        public DateTime? RequestedAt { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public DateTime? SharedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
