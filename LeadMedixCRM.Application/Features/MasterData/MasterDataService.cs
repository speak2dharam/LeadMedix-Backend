using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.MasterData.DTOs;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LeadMedixCRM.Application.Features.MasterData
{
    public class MasterDataService : IMasterDataService
    {
        private readonly ICountryRepository _countries;
        private readonly ICityRepository _cities;
        private readonly IAccreditationRepository _accreditations;
        private readonly IFileStorageService _fileStorage;
        private readonly IMediaFileRepository _mediaFile;

        public MasterDataService(ICountryRepository countries, ICityRepository cities, IAccreditationRepository accreditations, IFileStorageService fileStorage, IMediaFileRepository mediaFile)
        {
            _countries = countries;
            _cities = cities;
            _accreditations = accreditations;
            _fileStorage = fileStorage;
            _mediaFile = mediaFile;
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

        public async Task<List<AccreditationDto>> GetAccreditationsAsync()
        {
            var data = await _accreditations.GetAllAsync();
            var accreditationDtos = data
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .Select(x => new AccreditationDto(
                    x.Id,
                    x.Name,
                    x.Code,
                    x.Description,
                    x.IsActive,
                    LogoUrl: null
                ))
                .ToList();
            // ✅ Attach Logos (same pattern as hospitals)
            var ids = accreditationDtos.Select(x => x.Id).ToList();

            var logos = await _mediaFile.GetPrimaryListAsync(
                entityType: "Accredation",
                entityIds: ids,
                mediaType: "Logo"
            );

            var logoMap = logos.ToDictionary(x => x.EntityId, x => x.RelativePath);

            // records are immutable => create new copies using "with"
            accreditationDtos = accreditationDtos
                .Select(a => logoMap.TryGetValue(a.Id, out var logoUrl)
                    ? a with { LogoUrl = logoUrl }
                    : a)
                .ToList();

            return accreditationDtos;

        }

        public async Task<int> CreateAccreditationAsync(AccreditationDto dto)
        {
            var entity = new Accreditation
            {
                Name = dto.Name.Trim(),
                Code = string.IsNullOrWhiteSpace(dto.Code) ? null : dto.Code.Trim(),
                Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim(),
                IsActive = dto.IsActive
            };

            return await _accreditations.AddAsync(entity);
        }

        public async Task<bool> UpdateAccreditationAsync(int id, AccreditationDto dto)
        {
            var entity = await _accreditations.GetByIdAsync(id);
            if (entity == null || entity.IsDeleted) return false;

            entity.Name = dto.Name.Trim();
            entity.Code = string.IsNullOrWhiteSpace(dto.Code) ? null : dto.Code.Trim();
            entity.Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim();
            entity.IsActive = dto.IsActive;

            return await _accreditations.UpdateAsync(entity);
        }

        public Task<bool> DeleteAccreditationAsync(int id) => _accreditations.SoftDeleteAsync(id);

        public async Task<string?> UploadAccredationLogoAsync(int AccredationID, Stream stream, string originalFileName, string contentType)
        {
            var accredation = await _accreditations.GetByIdAsync(AccredationID);
            if (accredation == null || accredation.IsDeleted) return null;

            var folder = $"uploads/accredation/{AccredationID}/logo";
            var saved = await _fileStorage.SaveAsync(stream, originalFileName, contentType, folder);

            await _mediaFile.UnsetPrimaryAsync("Accredation", AccredationID, "Logo");

            await _mediaFile.AddAsync(new MediaFile
            {
                EntityType = "Accredation",
                EntityId = AccredationID,
                MediaType = "Logo",
                RelativePath = saved.relativePath,
                FileName = saved.fileName,
                ContentType = saved.contentType,
                Size = saved.size,
                IsPrimary = true
            });

            return saved.relativePath;
        }
    }
}
