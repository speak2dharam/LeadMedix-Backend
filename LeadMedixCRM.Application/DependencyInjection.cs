using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Features.Auth.Login;
using LeadMedixCRM.Application.Features.Hospitals;
using LeadMedixCRM.Application.Features.Leads;
using LeadMedixCRM.Application.Features.MasterData;
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
            services.AddScoped<ILeadService, LeadService>();

            services.AddScoped<IMasterDataService, MasterDataService>();
            services.AddScoped<IHospitalService, HospitalService>();

            return services;
        }
    }
}
