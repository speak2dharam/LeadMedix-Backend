using LeadMedixCRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Entities
{
    public class DoctorMembership : BaseEntity
    {
        public int DoctorId { get; set; }
        public string MembershipName { get; set; } = null!;
    }
}
