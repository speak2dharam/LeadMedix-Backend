using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Features.Doctors.DTOs.Requests;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Doctors
{
    public class DoctorSpecializationService:IDoctorSpecializationService
    {
        private readonly IDoctorSpecializationRepository _repo;
        private readonly ICurrentUserService _currentUser;

        public DoctorSpecializationService(
            IDoctorSpecializationRepository repo,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        public async Task<int> AddAsync(int doctorId, CreateDoctorSpecializationRequest dto)
        {
            var row = new DoctorSpecialization
            {
                DoctorId = doctorId,
                SpecializationName = dto.SpecializationName,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            };

            return await _repo.AddAsync(row);
        }

        public async Task DeleteAsync(int doctorId, int id)
        {
            var row = await _repo.GetByIdAsync(id) ?? throw new Exception("Speciality not found");
            if (row.DoctorId != doctorId) throw new Exception("Invalid doctor");

            row.UpdatedAt = DateTime.UtcNow;

            await _repo.SoftDeleteAsync(row);
        }

        public async Task<List<string>> GetAsync(int doctorId)
        {
            var rows = await _repo.GetByDoctorIdAsync(doctorId);
            return rows.Select(x => x.SpecializationName).ToList();
        }
    }
}
