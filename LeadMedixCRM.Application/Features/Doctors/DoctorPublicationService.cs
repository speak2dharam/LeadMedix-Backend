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
    public class DoctorPublicationService:IDoctorPublicationService
    {
        private readonly IDoctorPublicationRepository _repo;
        private readonly ICurrentUserService _currentUser;

        public DoctorPublicationService(
            IDoctorPublicationRepository repo,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        public async Task<int> AddAsync(int doctorId, CreateDoctorPublicationRequest dto)
        {
            var row = new DoctorPublication
            {
                DoctorId = doctorId,
                Title = dto.Title,
                Journal = dto.Journal,
                Year = dto.Year,
                Url = dto.Url,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            };

            return await _repo.AddAsync(row);
        }

        public async Task UpdateAsync(int doctorId, int id, UpdateDoctorPublicationRequest dto)
        {
            var row = await _repo.GetByIdAsync(id) ?? throw new Exception("Publication not found");
            if (row.DoctorId != doctorId) throw new Exception("Invalid doctor");

            row.Title = dto.Title;
            row.Journal = dto.Journal;
            row.Year = dto.Year;
            row.Url = dto.Url;

            row.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(row);
        }

        public async Task DeleteAsync(int doctorId, int id)
        {
            var row = await _repo.GetByIdAsync(id) ?? throw new Exception("Publication not found");
            if (row.DoctorId != doctorId) throw new Exception("Invalid doctor");

            row.UpdatedAt = DateTime.UtcNow;

            await _repo.SoftDeleteAsync(row);
        }

        public async Task<List<DoctorPublicationDto>> GetAsync(int doctorId)
        {
            var rows = await _repo.GetByDoctorIdAsync(doctorId);

            return rows.Select(x => new DoctorPublicationDto
            {
                Id = x.Id,
                Title = x.Title,
                Journal = x.Journal,
                Year = x.Year,
                Url = x.Url,

            }).ToList();
        }
    }
}
