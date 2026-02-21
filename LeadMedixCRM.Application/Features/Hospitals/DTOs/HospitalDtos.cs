using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Hospitals.DTOs
{
    public class HospitalListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        //public int CountryId { get; set; }
        //public int CityId { get; set; }
        public CountrySummaryDto Country { get; set; } = default!;
        public CitySummaryDto City { get; set; } = default!;
        public decimal? Rating { get; set; }
        public int? BedsCount { get; set; }
        public bool IsActive { get; set; }
        public string? LogoUrl { get; set; }
    }

    public class HospitalDetailDto : HospitalListItemDto
    {
        public string? Code { get; set; }
        public string? About { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public int? EstablishedYear { get; set; }

        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Landmark { get; set; }
        public string? Pincode { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public bool IsPartner { get; set; }
    }

    public class HospitalUpsertRequest
    {
        public string Name { get; set; } = default!;
        public string? Code { get; set; }
        public string? About { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }

        public decimal? Rating { get; set; }
        public int? BedsCount { get; set; }
        public int? EstablishedYear { get; set; }

        public int CountryId { get; set; }
        public int CityId { get; set; }

        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Landmark { get; set; }
        public string? Pincode { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public bool IsPartner { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class CountrySummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Iso2 { get; set; } = default!;
        public string? Iso3 { get; set; }
        public string PhoneCode { get; set; } = default!;
        public string? CurrencyCode { get; set; }
    }
    public class CitySummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
