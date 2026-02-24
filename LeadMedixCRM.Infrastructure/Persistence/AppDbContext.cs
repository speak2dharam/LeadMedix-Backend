using LeadMedixCRM.Domain.Entities;
using LeadMedixCRM.Domain.Entities.Leads;
using LeadMedixCRM.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Infrastructure.Persistence
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        //public DbSet<Country> Countries { get; set; }
        public DbSet<TreatmentCategory> TreatmentCategories { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<LeadSource> LeadSources { get; set; }
        //Lead
        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadAssignmentHistory> leadAssignmentHistories { get; set; }
        public DbSet<LeadHospitalReview> leadHospitalReviews { get; set; }
        public DbSet<LeadQuotation> leadQuotations { get; set; }
        public DbSet<LeadRequirement> leadRequirements { get; set; }
        public DbSet<LeadVIL> leadVILs { get; set; }
        public DbSet<LeadActivity> LeadActivities { get; set; }
        //Masters 
        public DbSet<HospitalReviewStatusMaster> hospitalReviewStatusMasters { get; set; }
        public DbSet<LeadCloseReasonMaster> leadCloseReasonMasters { get; set; }
        public DbSet<LeadDiscardReasonMaster> leadDiscardReasonMasters { get; set; }
        public DbSet<LeadRequirementStatusMaster> leadRequirementStatusMasters { get; set; }
        public DbSet<LeadRequirementTypeMaster> leadRequirementTypeMasters { get; set; }
        public DbSet<LeadStatusMaster> leadStatusMasters { get; set; }
        public DbSet<QuotationStatusMaster> quotationStatusMasters { get; set; }
        public DbSet<VILStatusMaster> vILStatusMasters { get; set; }
        //end master
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Country> Countries => Set<Country>();
        public DbSet<City> Cities => Set<City>();

        public DbSet<Hospital> Hospitals => Set<Hospital>();
        public DbSet<Accreditation> Accreditations { get; set; }
        public DbSet<HospitalAccreditation> HospitalAccreditations { get; set; }

        public DbSet<MediaFile> MediaFiles => Set<MediaFile>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<DoctorHospitalHistory> DoctorHospitalHistories => Set<DoctorHospitalHistory>();
        public DbSet<DoctorEducation> DoctorEducation => Set<DoctorEducation>();
        public DbSet<DoctorMembership> DoctorMembership => Set<DoctorMembership>();
        public DbSet<DoctorSpecialization> DoctorSpecialization => Set<DoctorSpecialization>();
        public DbSet<DoctorAward> DoctorAward => Set<DoctorAward>();
        public DbSet<DoctorPublication> DoctorPublication => Set<DoctorPublication>();
        public DbSet<DoctorFellowship> DoctorFellowship => Set<DoctorFellowship>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Soft delete filters
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<UserToken>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Country>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<TreatmentCategory>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Treatment>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<LeadSource>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Lead>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<LeadActivity>().HasQueryFilter(x => !x.IsDeleted);

            // Indexes (for duplicate check + filters)
            modelBuilder.Entity<Lead>().HasIndex(x => x.PhoneNormalized);
            modelBuilder.Entity<Lead>().HasIndex(x => x.EmailNormalized);
            modelBuilder.Entity<Lead>().HasIndex(x => new { x.Status, x.Temperature });
            modelBuilder.Entity<Lead>().HasIndex(x => x.AssignedToUserId);

            modelBuilder.Entity<LeadActivity>().HasIndex(x => x.LeadId);
            modelBuilder.Entity<LeadActivity>().HasIndex(x => x.NextFollowUpAt);

            modelBuilder.Entity<Role>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<UserRole>().HasQueryFilter(x => !x.IsDeleted);

            // prevent duplicates logically (even without FK)
            modelBuilder.Entity<UserRole>().HasIndex(x => new { x.UserId, x.RoleId }).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(x => x.Code).IsUnique();

            modelBuilder.Entity<LeadStatusMaster>(e =>
            {
                e.ToTable("LeadStatusMasters");
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                e.Property(x => x.Code).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<LeadRequirementTypeMaster>(e =>
            {
                e.ToTable("LeadRequirementTypeMasters");
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                e.Property(x => x.Code).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<LeadRequirementStatusMaster>(e =>
            {
                e.ToTable("LeadRequirementStatusMasters");
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                e.Property(x => x.Code).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<HospitalReviewStatusMaster>(e =>
            {
                e.ToTable("HospitalReviewStatusMasters");
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                e.Property(x => x.Code).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<QuotationStatusMaster>(e =>
            {
                e.ToTable("QuotationStatusMasters");
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                e.Property(x => x.Code).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<VILStatusMaster>(e =>
            {
                e.ToTable("VILStatusMasters");
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                e.Property(x => x.Code).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<LeadDiscardReasonMaster>(e =>
            {
                e.ToTable("LeadDiscardReasonMasters");
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                e.Property(x => x.Code).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<LeadCloseReasonMaster>(e =>
            {
                e.ToTable("LeadCloseReasonMasters");
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                e.Property(x => x.Code).HasMaxLength(100).IsRequired();
            });
            //LeadMastersSeed.Seed(modelBuilder);
        }

    }
}
