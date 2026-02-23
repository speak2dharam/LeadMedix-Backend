using LeadMedixCRM.Domain.Entities;
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

        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadActivity> LeadActivities { get; set; }
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
        }

    }
}
