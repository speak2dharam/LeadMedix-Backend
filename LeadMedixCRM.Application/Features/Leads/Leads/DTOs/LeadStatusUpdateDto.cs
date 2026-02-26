using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.Leads.DTOs
{
    public class LeadStatusUpdateDto
    {
        public int StatusId { get; set; } // LeadStatusMaster.Id
        public string? Remarks { get; set; }
    }
}
