using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Hospitals.DTOs;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Hospitals
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitals;
        private readonly IMediaFileRepository _media;
        private readonly IFileStorageService _files;
        private readonly ICurrentUserService _current;
        private readonly ICountryRepository _countryRepo;
        private readonly ICityRepository _cityRepo;
        private readonly IHospitalAccreditationRepository _hospitalAccreditationRepo;
        private readonly IAccreditationRepository? _accreditationRepo;

        public HospitalService(IHospitalRepository hospitals, IMediaFileRepository media, IFileStorageService files, ICurrentUserService current,
            ICountryRepository countryRepository,ICityRepository cityRepository, IHospitalAccreditationRepository hospitalAccreditationRepo, 
            IAccreditationRepository? accreditationRepo)
        {
            _hospitals = hospitals;
            _media = media;
            _files = files;
            _current = current;
            _countryRepo = countryRepository;
            _cityRepo = cityRepository;
            _hospitalAccreditationRepo = hospitalAccreditationRepo;
            _accreditationRepo = accreditationRepo;
        }

        public async Task<PaginatedResponse<HospitalListItemDto>> GetPagedAsync(PaginationRequest request)
        {
            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            // repo returns (items, totalRecords)
            var (items, totalRecords) = await _hospitals.GetPagedAsync(request);

            var countryIds = items.Select(x => x.CountryId).Distinct().ToList();
            var cityIds = items.Select(x => x.CityId).Distinct().ToList();

            var countries = await _countryRepo.GetByIdsAsync(countryIds);
            var cities = await _cityRepo.GetByIdsAsync(cityIds);

            var countryMap = countries.ToDictionary(x => x.Id);
            var cityMap = cities.ToDictionary(x => x.Id);

            var hospitalDtos = items
        .Where(x => !x.IsDeleted)
        .Select(x =>
        {
            countryMap.TryGetValue(x.CountryId, out var c);
            cityMap.TryGetValue(x.CityId, out var city);

            return new HospitalListItemDto
            {
                Id = x.Id,
                Name = x.Name,
                Rating = x.Rating,
                BedsCount = x.BedsCount,
                IsActive = x.IsActive,

                Country = c == null
                    ? new CountrySummaryDto
                    {
                        Id = x.CountryId,
                        Name = "Unknown",
                        Iso2 = "",
                        Iso3 = null,
                        PhoneCode = "",
                        CurrencyCode = null
                    }
                    : new CountrySummaryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Iso2 = c.Iso2,
                        Iso3 = c.Iso3,
                        PhoneCode = c.PhoneCode,
                        CurrencyCode = c.CurrencyCode
                    },

                City = city == null
                    ? new CitySummaryDto
                    {
                        Id = x.CityId,
                        Name = "Unknown"
                    }
                    : new CitySummaryDto
                    {
                        Id = city.Id,
                        Name = city.Name
                    }
            };
        })
        .ToList();

            // Attach Logos
            var ids = hospitalDtos.Select(x => x.Id).ToList();

            var logos = await _media.GetPrimaryListAsync(
                entityType: "Hospital",
                entityIds: ids,
                mediaType: "Logo");

            var logoMap = logos.ToDictionary(x => x.EntityId, x => x.RelativePath);

            foreach (var hospital in hospitalDtos)
            {
                if (logoMap.TryGetValue(hospital.Id, out var logoUrl))
                {
                    hospital.LogoUrl = logoUrl;
                }
            }

            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            return new PaginatedResponse<HospitalListItemDto>
            {
                Data = hospitalDtos,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        }

        public async Task<HospitalDetailDto?> GetByIdAsync(int id)
        {
            var h = await _hospitals.GetByIdAsync(id);
            if (h == null || h.IsDeleted) return null;

            var country = await _countryRepo.GetByIdAsync(h.CountryId);
            var city = await _cityRepo.GetByIdAsync(h.CityId);

            var logo = await _media.GetPrimaryAsync("Hospital", id, "Logo");

            return new HospitalDetailDto
            {
                Id = h.Id,
                Name = h.Name,
                Code = h.Code,
                About = h.About,
                Phone = h.Phone,
                Email = h.Email,
                Website = h.Website,
                Rating = h.Rating,
                BedsCount = h.BedsCount,
                EstablishedYear = h.EstablishedYear,
                Country = country == null
            ? new CountrySummaryDto
            {
                Id = h.CountryId,
                Name = "Unknown",
                Iso2 = "",
                Iso3 = null,
                PhoneCode = "",
                CurrencyCode = null
            }
            : new CountrySummaryDto
            {
                Id = country.Id,
                Name = country.Name,
                Iso2 = country.Iso2,
                Iso3 = country.Iso3,
                PhoneCode = country.PhoneCode,
                CurrencyCode = country.CurrencyCode
            },

                City = city == null
            ? new CitySummaryDto
            {
                Id = h.CityId,
                Name = "Unknown"
            }
            : new CitySummaryDto
            {
                Id = city.Id,
                Name = city.Name
            },
                AddressLine1 = h.AddressLine1,
                AddressLine2 = h.AddressLine2,
                Landmark = h.Landmark,
                Pincode = h.Pincode,
                Latitude = h.Latitude,
                Longitude = h.Longitude,
                IsPartner = h.IsPartner,
                IsActive = h.IsActive,
                LogoUrl = logo?.RelativePath
            };
        }

        public async Task<int> CreateAsync(HospitalUpsertRequest req)
        {
            var entity = new Hospital
            {
                Name = req.Name.Trim(),
                Code = string.IsNullOrWhiteSpace(req.Code) ? null : req.Code.Trim(),
                About = req.About,
                Phone = req.Phone,
                Email = req.Email,
                Website = req.Website,
                Rating = req.Rating,
                BedsCount = req.BedsCount,
                EstablishedYear = req.EstablishedYear,
                CountryId = req.CountryId,
                CityId = req.CityId,
                AddressLine1 = req.AddressLine1,
                AddressLine2 = req.AddressLine2,
                Landmark = req.Landmark,
                Pincode = req.Pincode,
                Latitude = req.Latitude,
                Longitude = req.Longitude,
                IsPartner = req.IsPartner,
                IsActive = req.IsActive
            };

            return await _hospitals.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, HospitalUpsertRequest req)
        {
            var entity = await _hospitals.GetByIdAsync(id);
            if (entity == null || entity.IsDeleted) return false;

            entity.Name = req.Name.Trim();
            entity.Code = string.IsNullOrWhiteSpace(req.Code) ? null : req.Code.Trim();
            entity.About = req.About;
            entity.Phone = req.Phone;
            entity.Email = req.Email;
            entity.Website = req.Website;
            entity.Rating = req.Rating;
            entity.BedsCount = req.BedsCount;
            entity.EstablishedYear = req.EstablishedYear;
            entity.CountryId = req.CountryId;
            entity.CityId = req.CityId;
            entity.AddressLine1 = req.AddressLine1;
            entity.AddressLine2 = req.AddressLine2;
            entity.Landmark = req.Landmark;
            entity.Pincode = req.Pincode;
            entity.Latitude = req.Latitude;
            entity.Longitude = req.Longitude;
            entity.IsPartner = req.IsPartner;
            entity.IsActive = req.IsActive;

            return await _hospitals.UpdateAsync(entity);
        }

        public Task<bool> DeleteAsync(int id) => _hospitals.SoftDeleteAsync(id);

        public async Task<string?> UploadLogoAsync(int hospitalId, Stream stream, string originalFileName, string contentType)
        {
            var hospital = await _hospitals.GetByIdAsync(hospitalId);
            if (hospital == null || hospital.IsDeleted) return null;

            var folder = $"uploads/hospitals/{hospitalId}/logo";
            var saved = await _files.SaveAsync(stream, originalFileName, contentType, folder);

            await _media.UnsetPrimaryAsync("Hospital", hospitalId, "Logo");

            await _media.AddAsync(new MediaFile
            {
                EntityType = "Hospital",
                EntityId = hospitalId,
                MediaType = "Logo",
                RelativePath = saved.relativePath,
                FileName = saved.fileName,
                ContentType = saved.contentType,
                Size = saved.size,
                IsPrimary = true
            });

            return saved.relativePath;
        }
        public async Task<bool> UpsertAccreditationsAsync(int hospitalId, List<HospitalAccreditationUpsertDto> items)
        {
            // Soft approach: upsert each mapping; not deleting missing ones automatically.
            foreach (var dto in items)
            {
                // ensure accreditation exists
                var acc = await _accreditationRepo.GetByIdAsync(dto.AccreditationId);
                if (acc == null) throw new Exception($"AccreditationId {dto.AccreditationId} not found.");

                var existing = await _hospitalAccreditationRepo.GetByHospitalAndAccreditationAsync(hospitalId, dto.AccreditationId);

                if (existing == null)
                {
                    var entity = new HospitalAccreditation
                    {
                        HospitalId = hospitalId,
                        AccreditationId = dto.AccreditationId,
                        CertificateNumber = dto.CertificateNumber,
                        AccreditedOn = dto.AccreditedOn,
                        ValidTill = dto.ValidTill,
                        IsActive = dto.IsActive,

                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = _current.UserId
                    };

                    await _hospitalAccreditationRepo.AddAsync(entity);
                }
                else
                {
                    existing.CertificateNumber = dto.CertificateNumber;
                    existing.AccreditedOn = dto.AccreditedOn;
                    existing.ValidTill = dto.ValidTill;
                    existing.IsActive = dto.IsActive;

                    existing.UpdatedAt = DateTime.UtcNow;

                    _hospitalAccreditationRepo.Update(existing);
                }
            }

            return true;
        }

        public async Task<List<HospitalAccreditationViewDto>> GetAccreditationsAsync(int hospitalId)
        {
            var result = new List<HospitalAccreditationViewDto>();

            var mappings = await _hospitalAccreditationRepo.GetByHospitalIdAsync(hospitalId);

            if (mappings == null || mappings.Count == 0)
                return result;

            var accIds = mappings.Select(x => x.AccreditationId).Distinct().ToList();

            var accList = new List<Accreditation>();
            foreach (var id in accIds)
            {
                var acc = await _accreditationRepo.GetByIdAsync(id);
                if (acc != null) accList.Add(acc);
            }

            // 3) Fetch logos from MediaFiles for these accreditation ids
            var logos = await _media.GetPrimaryListAsync(
                entityType: "Accredation",   // MUST match DB
                entityIds: accIds,
                mediaType: "Logo"              // MUST match DB
            );
            var logoMap = logos.ToDictionary(x => x.EntityId, x => x);

            foreach (var map in mappings)
            {
                var acc = accList.FirstOrDefault(x => x.Id == map.AccreditationId);
                if (acc == null) continue;

                logoMap.TryGetValue(acc.Id, out var logo);

                result.Add(new HospitalAccreditationViewDto
                {
                    AccreditationId = acc.Id,
                    AccreditationName = acc.Name,
                    AccreditationCode = acc.Code,

                    LogoUrl = logo?.RelativePath,
                    LogoMediaFileId = logo?.Id,

                    CertificateNumber = map.CertificateNumber,
                    AccreditedOn = map.AccreditedOn,
                    ValidTill = map.ValidTill,
                    IsActive = map.IsActive
                });
            }

            return result;
        }
    }
}
