using LeadMedixCRM.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ILeadHospitalReviewRepository
    {
        Task<LeadHospitalReview?> GetByIdAsync(int id);
        Task AddAsync(LeadHospitalReview entity);
        Task UpdateAsync(LeadHospitalReview entity);
        Task SoftDeleteAsync(LeadHospitalReview entity);

        Task<(List<LeadHospitalReview> Items, int Total)> GetPagedAsync(int pageNumber, int pageSize);
        Task<(List<LeadHospitalReview> Items, int Total)> GetPagedByLeadIdAsync(int leadId, int pageNumber, int pageSize);
    }
}
