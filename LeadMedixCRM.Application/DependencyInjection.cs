using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Features.Auth.Login;
using LeadMedixCRM.Application.Features.Doctors;
using LeadMedixCRM.Application.Features.Hospitals;
using LeadMedixCRM.Application.Features.LeadHospitalReviews;
using LeadMedixCRM.Application.Features.LeadMasters;
using LeadMedixCRM.Application.Features.LeadRequirements;
using LeadMedixCRM.Application.Features.Leads.LeadAssignment;
using LeadMedixCRM.Application.Features.Leads.LeadQuote;
using LeadMedixCRM.Application.Features.Leads.LeadVILs;
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

            services.AddScoped<IMasterDataService, MasterDataService>();
            services.AddScoped<IHospitalService, HospitalService>();

            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IDoctorAwardService, DoctorAwardService>();
            services.AddScoped<IDoctorEducationService, DoctorEducationService>();
            services.AddScoped<IDoctorFellowshipService, DoctorFellowshipService>();
            services.AddScoped<IDoctorHospitalHistoryService, DoctorHospitalHistoryService>();
            services.AddScoped<IDoctorMembershipService, DoctorMembershipService>();
            services.AddScoped<IDoctorPublicationService, DoctorPublicationService>();
            services.AddScoped<IDoctorSpecializationService, DoctorSpecializationService>();

            services.AddScoped<ILeadMastersService, LeadMastersService>();
            services.AddScoped<ILeadRequirementService, LeadRequirementService>();
            services.AddScoped<ILeadHospitalReviewService, LeadHospitalReviewService>();
            services.AddScoped<ILeadAssignmentHistoryService, LeadAssignmentHistoryService>();
            services.AddScoped<ILeadQuotationService, LeadQuotationService>();
            services.AddScoped<ILeadVILService, LeadVILService>();

            return services;
        }
    }
}
