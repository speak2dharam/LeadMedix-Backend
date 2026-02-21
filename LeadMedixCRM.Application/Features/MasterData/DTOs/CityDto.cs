using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.MasterData.DTOs
{
    public record CityDto(int Id, int CountryId, string Name, bool IsActive);
}
