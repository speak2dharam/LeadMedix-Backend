using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Features.MasterData.DTOs;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.MasterData
{
    public class MasterDataService : IMasterDataService
    {
        private readonly ICountryRepository _countries;
        private readonly ICityRepository _cities;

        public MasterDataService(ICountryRepository countries, ICityRepository cities)
        {
            _countries = countries;
            _cities = cities;
        }

        public async Task<List<CountryDto>> GetCountriesAsync()
        {
            var data = await _countries.GetAllAsync();
            return data.Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .Select(x => new CountryDto(x.Id, x.Name, x.Iso2, x.Iso3, x.PhoneCode, x.CurrencyCode, x.IsActive))
                .ToList();
        }

        public async Task<List<CityDto>> GetCitiesByCountryAsync(int countryId)
        {
            var data = await _cities.GetByCountryIdAsync(countryId);
            return data.Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .Select(x => new CityDto(x.Id, x.CountryId, x.Name, x.IsActive))
                .ToList();
        }

        public async Task<int> CreateCountryAsync(CountryDto dto)
        {
            var entity = new Country
            {
                Name = dto.Name.Trim(),
                Iso2 = dto.Iso2.Trim().ToUpperInvariant(),
                Iso3 = string.IsNullOrWhiteSpace(dto.Iso3) ? null : dto.Iso3.Trim().ToUpperInvariant(),
                PhoneCode = dto.PhoneCode.Trim(),
                CurrencyCode = string.IsNullOrWhiteSpace(dto.CurrencyCode) ? null : dto.CurrencyCode.Trim().ToUpperInvariant(),
                IsActive = dto.IsActive
            };
            return await _countries.AddAsync(entity);
        }

        public async Task<bool> UpdateCountryAsync(int id, CountryDto dto)
        {
            var entity = await _countries.GetByIdAsync(id);
            if (entity == null || entity.IsDeleted) return false;

            entity.Name = dto.Name.Trim();
            entity.Iso2 = dto.Iso2.Trim().ToUpperInvariant();
            entity.Iso3 = string.IsNullOrWhiteSpace(dto.Iso3) ? null : dto.Iso3.Trim().ToUpperInvariant();
            entity.PhoneCode = dto.PhoneCode.Trim();
            entity.CurrencyCode = string.IsNullOrWhiteSpace(dto.CurrencyCode) ? null : dto.CurrencyCode.Trim().ToUpperInvariant();
            entity.IsActive = dto.IsActive;

            return await _countries.UpdateAsync(entity);
        }

        public Task<bool> DeleteCountryAsync(int id) => _countries.SoftDeleteAsync(id);

        public async Task<int> CreateCityAsync(CityDto dto)
        {
            var entity = new City
            {
                CountryId = dto.CountryId,
                Name = dto.Name.Trim(),
                IsActive = dto.IsActive
            };
            return await _cities.AddAsync(entity);
        }

        public async Task<bool> UpdateCityAsync(int id, CityDto dto)
        {
            var entity = await _cities.GetByIdAsync(id);
            if (entity == null || entity.IsDeleted) return false;

            entity.CountryId = dto.CountryId;
            entity.Name = dto.Name.Trim();
            entity.IsActive = dto.IsActive;

            return await _cities.UpdateAsync(entity);
        }

        public Task<bool> DeleteCityAsync(int id) => _cities.SoftDeleteAsync(id);
    }
}
