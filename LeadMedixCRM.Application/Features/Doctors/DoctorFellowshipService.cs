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
    public class DoctorFellowshipService:IDoctorFellowshipService
    {
        private readonly IDoctorFellowshipRepository _repo;
        private readonly ICurrentUserService _currentUser;

        public DoctorFellowshipService(
            IDoctorFellowshipRepository repo,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        public async Task<int> AddAsync(int doctorId, CreateDoctorFellowshipRequest dto)
        {
            var row = new DoctorFellowship
            {
                DoctorId = doctorId,
                Title = dto.Title,
                Organization = dto.Organization,
                Country = dto.Country,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            };

            return await _repo.AddAsync(row);
        }

        public async Task UpdateAsync(int doctorId, int id, UpdateDoctorFellowshipRequest dto)
        {
            var row = await _repo.GetByIdAsync(id) ?? throw new Exception("Fellowship not found");
            if (row.DoctorId != doctorId) throw new Exception("Invalid doctor");

            row.Title = dto.Title;
            row.Organization = dto.Organization;
            row.Country = dto.Country;
            row.FromDate = dto.FromDate;
            row.ToDate = dto.ToDate;

            row.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(row);
        }

        public async Task DeleteAsync(int doctorId, int id)
        {
            var row = await _repo.GetByIdAsync(id) ?? throw new Exception("Fellowship not found");
            if (row.DoctorId != doctorId) throw new Exception("Invalid doctor");

            row.UpdatedAt = DateTime.UtcNow;

            await _repo.SoftDeleteAsync(row);
        }

        public async Task<List<DoctorFellowshipDto>> GetAsync(int doctorId)
        {
            var rows = await _repo.GetByDoctorIdAsync(doctorId);

            return rows.Select(x => new DoctorFellowshipDto
            {
                Id = x.Id,
                Title = x.Title,
                Organization = x.Organization,
                Country = x.Country,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
            }).ToList();
        }
    }
}
