using LeadMedixCRM.Application.Features.Doctors.DTOs;
using LeadMedixCRM.Application.Features.Doctors.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface IDoctorHospitalHistoryService
    {
        Task<int> AddAsync(int doctorId, CreateDoctorHospitalHistoryRequest dto);
        Task UpdateAsync(int doctorId, int id, UpdateDoctorHospitalHistoryRequest dto);
        Task DeleteAsync(int doctorId, int id);
        Task<List<DoctorHospitalHistoryDto>> GetAsync(int doctorId);
    }
}
