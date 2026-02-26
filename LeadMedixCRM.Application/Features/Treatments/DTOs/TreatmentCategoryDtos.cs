using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Treatments.DTOs
{
    public class TreatmentCategoryCreateDto
    {
        public string Name { get; set; } = default!;
        public int SortOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }

    public class TreatmentCategoryUpdateDto : TreatmentCategoryCreateDto { }

    public class TreatmentCategoryListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
