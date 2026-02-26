using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.Leads.DTOs
{
    public class LeadCloseDto
    {
        public int CloseReasonId { get; set; }
        public string? Remarks { get; set; }
    }
}
