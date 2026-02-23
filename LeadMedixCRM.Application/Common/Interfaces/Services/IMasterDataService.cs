using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.MasterData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface IMasterDataService
    {
        Task<List<CountryDto>> GetCountriesAsync();
        Task<List<CityDto>> GetCitiesByCountryAsync(int countryId);

        Task<int> CreateCountryAsync(CountryDto dto);
        Task<bool> UpdateCountryAsync(int id, CountryDto dto);
        Task<bool> DeleteCountryAsync(int id);

        Task<int> CreateCityAsync(CityDto dto);
        Task<bool> UpdateCityAsync(int id, CityDto dto);
        Task<bool> DeleteCityAsync(int id);

        ///Accreditation
        Task<List<AccreditationDto>> GetAccreditationsAsync();
        Task<int> CreateAccreditationAsync(AccreditationDto dto);
        Task<bool> UpdateAccreditationAsync(int id, AccreditationDto dto);
        Task<bool> DeleteAccreditationAsync(int id);
        Task<string?> UploadAccredationLogoAsync(int AccredationID, Stream stream, string originalFileName, string contentType);
    }
}
