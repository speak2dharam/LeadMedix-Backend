using LeadMedixCRM.Application.Features.Doctors.DTOs;
using LeadMedixCRM.Application.Features.Doctors.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface IDoctorSpecializationService
    {
        Task<int> AddAsync(int doctorId, CreateDoctorSpecializationRequest dto);
        Task DeleteAsync(int doctorId, int id);
        Task<List<string>> GetAsync(int doctorId);
    }
}
