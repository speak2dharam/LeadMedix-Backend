using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadMedixCRM.Domain.Common;

namespace LeadMedixCRM.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string PasswordHash { get; set; }

        public bool IsActive { get; set; } = true;

        public int RoleId { get; set; }
    }
}
