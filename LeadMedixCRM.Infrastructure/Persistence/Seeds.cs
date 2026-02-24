using LeadMedixCRM.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Infrastructure.Persistence
{
    public static class LeadMastersSeed
    {
        private static readonly DateTime SeedDate = new DateTime(2026, 01, 01);

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeadStatusMaster>().HasData(
                new LeadStatusMaster { Id = 1, Name = "New", Code = "NEW", IsActive = true, SortOrder = 1, CreatedAt = SeedDate },
                new LeadStatusMaster { Id = 2, Name = "Contacted", Code = "CONTACTED", IsActive = true, SortOrder = 2, CreatedAt = SeedDate },
                new LeadStatusMaster { Id = 3, Name = "Qualified", Code = "QUALIFIED", IsActive = true, SortOrder = 3, CreatedAt = SeedDate },
                new LeadStatusMaster { Id = 4, Name = "Quote Requested", Code = "QUOTE_REQUESTED", IsActive = true, SortOrder = 4, CreatedAt = SeedDate },
                new LeadStatusMaster { Id = 5, Name = "Quote Received", Code = "QUOTE_RECEIVED", IsActive = true, SortOrder = 5, CreatedAt = SeedDate },
                new LeadStatusMaster { Id = 6, Name = "Shared With Patient", Code = "SHARED", IsActive = true, SortOrder = 6, CreatedAt = SeedDate },
                new LeadStatusMaster { Id = 7, Name = "Converted", Code = "CONVERTED", IsActive = true, SortOrder = 7, CreatedAt = SeedDate },
                new LeadStatusMaster { Id = 8, Name = "Lost", Code = "LOST", IsActive = true, SortOrder = 8, CreatedAt = SeedDate }
            );

            modelBuilder.Entity<LeadRequirementTypeMaster>().HasData(
                new LeadRequirementTypeMaster { Id = 1, Name = "Passport", Code = "PASSPORT", IsActive = true, SortOrder = 1, CreatedAt = SeedDate },
                new LeadRequirementTypeMaster { Id = 2, Name = "Medical Reports", Code = "MED_REPORTS", IsActive = true, SortOrder = 2, CreatedAt = SeedDate },
                new LeadRequirementTypeMaster { Id = 3, Name = "Imaging (CT/MRI/X-Ray)", Code = "IMAGING", IsActive = true, SortOrder = 3, CreatedAt = SeedDate },
                new LeadRequirementTypeMaster { Id = 4, Name = "Blood Tests", Code = "BLOOD_TESTS", IsActive = true, SortOrder = 4, CreatedAt = SeedDate }
            );

            modelBuilder.Entity<LeadRequirementStatusMaster>().HasData(
                new LeadRequirementStatusMaster { Id = 1, Name = "Pending", Code = "PENDING", IsActive = true, SortOrder = 1, CreatedAt = SeedDate },
                new LeadRequirementStatusMaster { Id = 2, Name = "Requested", Code = "REQUESTED", IsActive = true, SortOrder = 2, CreatedAt = SeedDate },
                new LeadRequirementStatusMaster { Id = 3, Name = "Received", Code = "RECEIVED", IsActive = true, SortOrder = 3, CreatedAt = SeedDate },
                new LeadRequirementStatusMaster { Id = 4, Name = "Rejected", Code = "REJECTED", IsActive = true, SortOrder = 4, CreatedAt = SeedDate },
                new LeadRequirementStatusMaster { Id = 5, Name = "Verified", Code = "VERIFIED", IsActive = true, SortOrder = 5, CreatedAt = SeedDate }
            );

            modelBuilder.Entity<HospitalReviewStatusMaster>().HasData(
                new HospitalReviewStatusMaster { Id = 1, Name = "Not Sent", Code = "NOT_SENT", IsActive = true, SortOrder = 1, CreatedAt = SeedDate },
                new HospitalReviewStatusMaster { Id = 2, Name = "Sent", Code = "SENT", IsActive = true, SortOrder = 2, CreatedAt = SeedDate },
                new HospitalReviewStatusMaster { Id = 3, Name = "In Review", Code = "IN_REVIEW", IsActive = true, SortOrder = 3, CreatedAt = SeedDate },
                new HospitalReviewStatusMaster { Id = 4, Name = "Completed", Code = "COMPLETED", IsActive = true, SortOrder = 4, CreatedAt = SeedDate }
            );

            modelBuilder.Entity<QuotationStatusMaster>().HasData(
                new QuotationStatusMaster { Id = 1, Name = "Not Requested", Code = "NOT_REQUESTED", IsActive = true, SortOrder = 1, CreatedAt = SeedDate },
                new QuotationStatusMaster { Id = 2, Name = "Requested", Code = "REQUESTED", IsActive = true, SortOrder = 2, CreatedAt = SeedDate },
                new QuotationStatusMaster { Id = 3, Name = "Received", Code = "RECEIVED", IsActive = true, SortOrder = 3, CreatedAt = SeedDate },
                new QuotationStatusMaster { Id = 4, Name = "Shared With Patient", Code = "SHARED", IsActive = true, SortOrder = 4, CreatedAt = SeedDate },
                new QuotationStatusMaster { Id = 5, Name = "Accepted", Code = "ACCEPTED", IsActive = true, SortOrder = 5, CreatedAt = SeedDate },
                new QuotationStatusMaster { Id = 6, Name = "Rejected", Code = "REJECTED", IsActive = true, SortOrder = 6, CreatedAt = SeedDate }
            );

            modelBuilder.Entity<VILStatusMaster>().HasData(
                new VILStatusMaster { Id = 1, Name = "Not Initiated", Code = "NOT_INITIATED", IsActive = true, SortOrder = 1, CreatedAt = SeedDate },
                new VILStatusMaster { Id = 2, Name = "Requested", Code = "REQUESTED", IsActive = true, SortOrder = 2, CreatedAt = SeedDate },
                new VILStatusMaster { Id = 3, Name = "Issued", Code = "ISSUED", IsActive = true, SortOrder = 3, CreatedAt = SeedDate },
                new VILStatusMaster { Id = 4, Name = "Rejected", Code = "REJECTED", IsActive = true, SortOrder = 4, CreatedAt = SeedDate }
            );

            modelBuilder.Entity<LeadDiscardReasonMaster>().HasData(
                new LeadDiscardReasonMaster { Id = 1, Name = "Duplicate Lead", Code = "DUPLICATE", IsActive = true, SortOrder = 1, CreatedAt = SeedDate },
                new LeadDiscardReasonMaster { Id = 2, Name = "Invalid Contact", Code = "INVALID_CONTACT", IsActive = true, SortOrder = 2, CreatedAt = SeedDate },
                new LeadDiscardReasonMaster { Id = 3, Name = "Not Eligible", Code = "NOT_ELIGIBLE", IsActive = true, SortOrder = 3, CreatedAt = SeedDate }
            );

            modelBuilder.Entity<LeadCloseReasonMaster>().HasData(
                new LeadCloseReasonMaster { Id = 1, Name = "Converted", Code = "CONVERTED", IsActive = true, SortOrder = 1, CreatedAt = SeedDate },
                new LeadCloseReasonMaster { Id = 2, Name = "Patient Not Responding", Code = "NO_RESPONSE", IsActive = true, SortOrder = 2, CreatedAt = SeedDate },
                new LeadCloseReasonMaster { Id = 3, Name = "Budget Issue", Code = "BUDGET", IsActive = true, SortOrder = 3, CreatedAt = SeedDate },
                new LeadCloseReasonMaster { Id = 4, Name = "Chose Another Provider", Code = "OTHER_PROVIDER", IsActive = true, SortOrder = 4, CreatedAt = SeedDate }
            );
        }
    }
}
