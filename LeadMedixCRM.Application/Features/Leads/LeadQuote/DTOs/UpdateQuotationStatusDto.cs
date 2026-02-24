using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.LeadQuote.DTOs
{
    public class UpdateQuotationStatusDto
    {
        public int Id { get; set; }
        public int QuotationStatusId { get; set; }
    }
}
