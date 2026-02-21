using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class Hospital : BaseEntity
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

        public bool IsPartner { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
