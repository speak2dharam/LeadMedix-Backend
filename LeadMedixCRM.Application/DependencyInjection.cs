using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Features.Auth.Login;
using LeadMedixCRM.Application.Features.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
