using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }   // FK concept only
        public int RoleId { get; set; }   // FK concept only
    }
}
