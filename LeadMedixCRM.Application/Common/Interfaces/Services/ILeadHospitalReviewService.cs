using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.LeadHospitalReviews.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ILeadHospitalReviewService
    {
        Task<LeadHospitalReviewDto> CreateAsync(CreateLeadHospitalReviewRequest request);
        Task<LeadHospitalReviewDto> UpdateAsync(int id, UpdateLeadHospitalReviewRequest request);
        Task<bool> DeleteAsync(int id);

        Task<LeadHospitalReviewDto> GetByIdAsync(int id);

        Task<PaginatedResponse<LeadHospitalReviewDto>> GetPagedAsync(PaginationRequest request);
        Task<PaginatedResponse<LeadHospitalReviewDto>> GetPagedByLeadIdAsync(int leadId, PaginationRequest request);
    }
}
