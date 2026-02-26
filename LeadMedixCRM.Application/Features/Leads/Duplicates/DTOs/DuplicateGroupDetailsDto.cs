using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.Duplicates.DTOs
{
    public class DuplicateGroupDetailsDto
    {
        public DuplicateLeadItemDto Parent { get; set; } = default!;
        public List<DuplicateLeadItemDto> Duplicates { get; set; } = new();
    }
}
