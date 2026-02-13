using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class UserToken : BaseEntity
    {
        public int UserId { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime AccessTokenExpiresAt { get; set; }

        public DateTime RefreshTokenExpiresAt { get; set; }

        public bool IsRevoked { get; set; } = false;

        public string? CreatedByIp { get; set; }

        public string? DeviceInfo { get; set; }

        public bool IsActive { get; set; } = true;
    }

}
