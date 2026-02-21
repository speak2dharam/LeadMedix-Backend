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
    }
}
