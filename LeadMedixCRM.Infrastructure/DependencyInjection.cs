using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Infrastructure.Persistence;
using LeadMedixCRM.Infrastructure.Repositories;
using LeadMedixCRM.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
