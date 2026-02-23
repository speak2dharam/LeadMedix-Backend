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
    public class DoctorHospitalHistoryService : IDoctorHospitalHistoryService
    {
        private readonly IDoctorHospitalHistoryRepository _repo;
        private readonly ILookupRepository _lookupRepo;
        private readonly ICurrentUserService _currentUser;

        public DoctorHospitalHistoryService(
            IDoctorHospitalHistoryRepository repo,
            ILookupRepository lookupRepo,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _lookupRepo = lookupRepo;
            _currentUser = currentUser;
        }

        public async Task<int> AddAsync(int doctorId, CreateDoctorHospitalHistoryRequest dto)
        {
            var row = new DoctorHospitalHistory
            {
                DoctorId = doctorId,
                HospitalId = dto.HospitalId,
                DesignationName = dto.DesignationName,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            };

            return await _repo.AddAsync(row);
        }

        public async Task UpdateAsync(int doctorId, int id, UpdateDoctorHospitalHistoryRequest dto)
        {
            var row = await _repo.GetByIdAsync(id) ?? throw new Exception("Hospital history not found");
            if (row.DoctorId != doctorId) throw new Exception("Invalid doctor");

            row.HospitalId = dto.HospitalId;
            row.DesignationName = dto.DesignationName;
            row.FromDate = dto.FromDate;
            row.ToDate = dto.ToDate;
            row.Notes = dto.Notes;

            row.UpdatedAt = DateTime.UtcNow;
            row.UpdatedBy = _currentUser.UserId;

            await _repo.UpdateAsync(row);
        }

        public async Task DeleteAsync(int doctorId, int id)
        {
            var row = await _repo.GetByIdAsync(id) ?? throw new Exception("Hospital history not found");
            if (row.DoctorId != doctorId) throw new Exception("Invalid doctor");

            row.UpdatedAt = DateTime.UtcNow;
            row.UpdatedBy = _currentUser.UserId;

            await _repo.SoftDeleteAsync(row);
        }

        public async Task<List<DoctorHospitalHistoryDto>> GetAsync(int doctorId)
        {
            var rows = await _repo.GetByDoctorIdAsync(doctorId);

            var hospitalIds = rows.Select(x => x.HospitalId).Distinct().ToList();
            var hospitalMap = await _lookupRepo.GetHospitalNamesByIdsAsync(hospitalIds);

            return rows.Select(x => new DoctorHospitalHistoryDto
            {
                Id = x.Id,
                Hospital = new LookupDto
                {
                    Id = x.HospitalId,
                    Name = hospitalMap.TryGetValue(x.HospitalId, out var n) ? n : "Unknown"
                },
                DesignationName = x.DesignationName,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                Notes = x.Notes
            }).ToList();
        }
    }
}
