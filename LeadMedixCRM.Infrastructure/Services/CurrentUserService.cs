using LeadMedixCRM.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Infrastructure.Services
{
    public class CurrentUserService:ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        private ClaimsPrincipal? User => _contextAccessor.HttpContext?.User;

        public int? UserId =>
            int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier), out var id)
                ? id
                : null;

        public string? Email =>
            User?.FindFirstValue(ClaimTypes.Email);

        public string? Name =>
            User?.FindFirstValue(ClaimTypes.Name);

        public string? Role =>
            User?.FindFirstValue(ClaimTypes.Role);

        public string? Token =>
            _contextAccessor.HttpContext?
                .Request.Headers["Authorization"]
                .ToString()
                .Replace("Bearer ", "");
    }
}
