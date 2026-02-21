using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.MasterData.DTOs
{
    public record CountryDto(int Id, string Name, string Iso2, string? Iso3, string PhoneCode, string? CurrencyCode, bool IsActive);
}
