using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Users.DTOs
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
    }
}
