using LeadMedixCRM.Application.Features.Doctors.DTOs;
using LeadMedixCRM.Application.Features.Doctors.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface IDoctorEducationService
    {
        Task<int> AddAsync(int doctorId, CreateDoctorEducationRequest dto);
        Task UpdateAsync(int doctorId, int id, UpdateDoctorEducationRequest dto);
        Task DeleteAsync(int doctorId, int id);
        Task<List<DoctorEducationDto>> GetAsync(int doctorId);
    }
}
