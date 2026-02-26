using LeadMedixCRM.Application.Common.Interfaces;
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
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            // If you add role CRUD:
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IFileStorageService, FileStorageService>();

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IAccreditationRepository, AccreditationRepository>();
            services.AddScoped<IHospitalAccreditationRepository, HospitalAccreditationRepository>();
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

            services.AddScoped(typeof(IMasterRepository<>), typeof(MasterRepository<>));
            services.AddScoped<ILeadRequirementRepository, LeadRequirementRepository>();
            services.AddScoped<ILeadHospitalReviewRepository, LeadHospitalReviewRepository>();
            services.AddScoped<ILeadAssignmentHistoryRepository, LeadAssignmentHistoryRepository>();
            services.AddScoped<ILeadQuotationRepository, LeadQuotationRepository>();
            services.AddScoped<ILeadVILRepository, LeadVILRepository>();

            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<ILeadActivityRepository, LeadActivityRepository>();
            services.AddScoped<ITreatmentCategoryRepository, TreatmentCategoryRepository>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();
            services.AddScoped<ILeadDuplicateRepository, LeadDuplicateRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
