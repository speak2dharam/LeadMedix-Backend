using LeadMedixCRM.Application.Common.Interfaces;
using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Infrastructure.Files;
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
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<ILeadActivityRepository, LeadActivityRepository>();

            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            // If you add role CRUD:
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IFileStorage, LocalFileStorage>();

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IMediaFileRepository, MediaFileRepository>();

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorHospitalHistoryRepository, DoctorHospitalHistoryRepository>();
            services.AddScoped<IDoctorEducationRepository, DoctorEducationRepository>();
            services.AddScoped<IDoctorMembershipRepository, DoctorMembershipRepository>();
            services.AddScoped<IDoctorSpecializationRepository, DoctorSpecializationRepository>();
            services.AddScoped<IDoctorAwardRepository, DoctorAwardRepository>();
            services.AddScoped<IDoctorPublicationRepository, DoctorPublicationRepository>();
            services.AddScoped<IDoctorFellowshipRepository, DoctorFellowshipRepository>();
            services.AddScoped<ILookupRepository, LookupRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
