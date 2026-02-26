using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Treatments.DTOs
{
    public class TreatmentCreateDto
    {
        public int TreatmentCategoryId { get; set; }
        public string Name { get; set; } = default!;
        public int SortOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }

    public class TreatmentUpdateDto : TreatmentCreateDto { }

    public class TreatmentListItemDto
    {
        public int Id { get; set; }
        public int TreatmentCategoryId { get; set; }
        public string Name { get; set; } = default!;
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
