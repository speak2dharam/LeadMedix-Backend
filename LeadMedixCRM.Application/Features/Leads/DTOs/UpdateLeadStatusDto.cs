using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.DTOs
{
    public class UpdateLeadStatusDto
    {
        [Required] public int Status { get; set; }
        public int? Temperature { get; set; } // optionally update temp too
    }
}
