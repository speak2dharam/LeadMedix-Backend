using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.MasterData.DTOs
{
    public record AccreditationDto(
        int Id,
        string Name,
        string? Code,
        string? Description,
        bool IsActive,
        string? LogoUrl
    );
}
