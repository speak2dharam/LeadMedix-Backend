using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Hospitals.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface IHospitalService
    {
        //Task<List<HospitalListItemDto>> GetAllAsync(string? search);
        Task<PaginatedResponse<HospitalListItemDto>> GetPagedAsync(PaginationRequest request);
        Task<HospitalDetailDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(HospitalUpsertRequest req);
        Task<bool> UpdateAsync(int id, HospitalUpsertRequest req);
        Task<bool> DeleteAsync(int id);

        Task<string?> UploadLogoAsync(int hospitalId, Stream stream, string originalFileName, string contentType);
    }
}
