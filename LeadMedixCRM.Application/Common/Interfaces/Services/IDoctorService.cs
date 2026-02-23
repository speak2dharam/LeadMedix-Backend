using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Doctors.DTOs;
using LeadMedixCRM.Application.Features.Doctors.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface IDoctorService
    {
        Task<int> CreateAsync(CreateDoctorRequest dto);
        Task UpdateAsync(int id, UpdateDoctorRequest dto);
        Task DeleteAsync(int id);

        Task<DoctorProfileDto> GetProfileAsync(int id);
        Task<PaginatedResponse<DoctorListItemDto>> GetPagedAsync(PaginationRequest request);
    }
}
