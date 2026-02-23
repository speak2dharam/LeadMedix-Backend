using LeadMedixCRM.Application.Features.Doctors.DTOs;
using LeadMedixCRM.Application.Features.Doctors.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface IDoctorFellowshipService
    {
        Task<int> AddAsync(int doctorId, CreateDoctorFellowshipRequest dto);
        Task UpdateAsync(int doctorId, int id, UpdateDoctorFellowshipRequest dto);
        Task DeleteAsync(int doctorId, int id);
        Task<List<DoctorFellowshipDto>> GetAsync(int doctorId);
    }
}
