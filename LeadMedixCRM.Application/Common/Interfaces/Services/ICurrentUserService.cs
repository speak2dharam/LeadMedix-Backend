using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        string? Email { get; }
        string? Name { get; }
        string? Role { get; }
        string? Token { get; }
    }
}
