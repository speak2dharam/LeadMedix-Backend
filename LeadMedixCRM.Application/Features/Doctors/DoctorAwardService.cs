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
    public class DoctorAwardService:IDoctorAwardService
    {
        private readonly IDoctorAwardRepository _repo;
        private readonly ICurrentUserService _currentUser;

        public DoctorAwardService(
            IDoctorAwardRepository repo,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        public async Task<int> AddAsync(int doctorId, CreateDoctorAwardRequest dto)
        {
            var row = new DoctorAward
            {
                DoctorId = doctorId,
               Title = dto.Title,
               Year = dto.Year,
               Issuer = dto.Issuer,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            };

            return await _repo.AddAsync(row);
        }

        public async Task UpdateAsync(int doctorId, int id, UpdateDoctorAwardRequest dto)
        {
            var row = await _repo.GetByIdAsync(id) ?? throw new Exception("Award not found");
            if (row.DoctorId != doctorId) throw new Exception("Invalid doctor");

            row.Title = dto.Title;
            row.Year = dto.Year;
            row.Issuer = dto.Issuer;

            row.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(row);
        }

        public async Task DeleteAsync(int doctorId, int id)
        {
            var row = await _repo.GetByIdAsync(id) ?? throw new Exception("Award not found");
            if (row.DoctorId != doctorId) throw new Exception("Invalid doctor");

            row.UpdatedAt = DateTime.UtcNow;

            await _repo.SoftDeleteAsync(row);
        }

        public async Task<List<DoctorAwardDto>> GetAsync(int doctorId)
        {
            var rows = await _repo.GetByDoctorIdAsync(doctorId);

            return rows.Select(x => new DoctorAwardDto
            {
                Id = x.Id,
                Title = x.Title,
                Year = x.Year,
                Issuer = x.Issuer
            }).ToList();
        }
    }
}
