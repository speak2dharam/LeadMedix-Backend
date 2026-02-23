using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Features.Doctors.DTOs;
using LeadMedixCRM.Application.Features.Doctors.DTOs.Requests;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Doctors
{
    public class DoctorEducationService:IDoctorEducationService
    {
        private readonly IDoctorEducationRepository _repo;
        private readonly ICurrentUserService _currentUser;

        public DoctorEducationService(
            IDoctorEducationRepository repo,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        public async Task<int> AddAsync(int doctorId, CreateDoctorEducationRequest dto)
        {
            var row = new DoctorEducation
            {
                DoctorId = doctorId,
                Degree = dto.Degree,
                Institute = dto.Institute,
                FromYear = dto.FromYear,
                ToYear = dto.ToYear,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            };

            return await _repo.AddAsync(row);
        }

        public async Task UpdateAsync(int doctorId, int id, UpdateDoctorEducationRequest dto)
        {
            var row = await _repo.GetByIdAsync(id) ?? throw new Exception("Education not found");
            if (row.DoctorId != doctorId) throw new Exception("Invalid doctor");

            row.Degree = dto.Degree;
            row.Institute = dto.Institute;
            row.FromYear = dto.FromYear;
            row.ToYear = dto.ToYear;

            row.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(row);
        }

        public async Task DeleteAsync(int doctorId, int id)
        {
            var row = await _repo.GetByIdAsync(id) ?? throw new Exception("Education not found");
            if (row.DoctorId != doctorId) throw new Exception("Invalid doctor");

            row.UpdatedAt = DateTime.UtcNow;

            await _repo.SoftDeleteAsync(row);
        }

        public async Task<List<DoctorEducationDto>> GetAsync(int doctorId)
        {
            var rows = await _repo.GetByDoctorIdAsync(doctorId);

            return rows.Select(x => new DoctorEducationDto
            {
                Id = x.Id,
                Degree = x.Degree,
                Institute = x.Institute,
                FromYear = x.FromYear,
                ToYear = x.ToYear,
            }).ToList();
        }
    }
}
