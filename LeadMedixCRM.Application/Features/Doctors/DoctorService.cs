using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
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
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepo;
        private readonly IDoctorHospitalHistoryRepository _historyRepo;
        private readonly IDoctorEducationRepository _educationRepo;
        private readonly IDoctorMembershipRepository _membershipRepo;
        private readonly IDoctorSpecializationRepository _specializationRepo;
        private readonly IDoctorAwardRepository _awardRepo;
        private readonly IDoctorPublicationRepository _publicationRepo;
        private readonly IDoctorFellowshipRepository _fellowshipRepo;

        private readonly ICurrentUserService _currentUser;

        private readonly ILookupRepository _lookupRepo;

        public DoctorService(
            IDoctorRepository doctorRepo,
            IDoctorHospitalHistoryRepository historyRepo,
            IDoctorEducationRepository educationRepo,
            IDoctorMembershipRepository membershipRepo,
            IDoctorSpecializationRepository specializationRepo,
            IDoctorAwardRepository awardRepo,
            IDoctorPublicationRepository publicationRepo,
            IDoctorFellowshipRepository fellowshipRepo,
            ICurrentUserService currentUser,
            ILookupRepository lookupRepo
        )
        {
            _doctorRepo = doctorRepo;
            _historyRepo = historyRepo;
            _educationRepo = educationRepo;
            _membershipRepo = membershipRepo;
            _specializationRepo = specializationRepo;
            _awardRepo = awardRepo;
            _publicationRepo = publicationRepo;
            _fellowshipRepo = fellowshipRepo;
            _currentUser = currentUser;
            _lookupRepo = lookupRepo;
        }

        public async Task<int> CreateAsync(CreateDoctorRequest dto)
        {
            var doctor = new Doctor
            {
                Name = dto.Name,
                ProfileOverview = dto.ProfileOverview,
                TotalExperienceYears = dto.TotalExperienceYears,
                CurrentHospitalId = dto.CurrentHospitalId,
                CurrentDesignationName = dto.CurrentDesignationName,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            };

            return await _doctorRepo.AddAsync(doctor);
        }

        public async Task UpdateAsync(int id, UpdateDoctorRequest dto)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id) ?? throw new Exception("Doctor not found");

            doctor.Name = dto.Name;
            doctor.ProfileOverview = dto.ProfileOverview;
            doctor.TotalExperienceYears = dto.TotalExperienceYears;
            doctor.CurrentHospitalId = dto.CurrentHospitalId;
            doctor.CurrentDesignationName = dto.CurrentDesignationName;

            doctor.UpdatedAt = DateTime.UtcNow;
            doctor.UpdatedBy = _currentUser.UserId;

            await _doctorRepo.UpdateAsync(doctor);
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id) ?? throw new Exception("Doctor not found");
            doctor.UpdatedBy = _currentUser.UserId;
            await _doctorRepo.SoftDeleteAsync(doctor);
        }

        public async Task<PaginatedResponse<DoctorListItemDto>> GetPagedAsync(PaginationRequest request)
        {
            var (data, total) = await _doctorRepo.GetPagedAsync(request.PageNumber, request.PageSize);

            // Collect hospital ids for lookup
            var hospitalIds = data
                .Where(d => d.CurrentHospitalId.HasValue)
                .Select(d => d.CurrentHospitalId!.Value)
                .Distinct()
                .ToList();

            var hospitalMap = await _lookupRepo.GetHospitalNamesByIdsAsync(hospitalIds);


            var mapped = data.Select(d => new DoctorListItemDto
            {
                Id = d.Id,
                Name = d.Name,
                TotalExperienceYears = d.TotalExperienceYears,
                CurrentDesignationName = d.CurrentDesignationName,
                CurrentHospital = d.CurrentHospitalId.HasValue && hospitalMap.TryGetValue(d.CurrentHospitalId.Value, out var hn)
                    ? new LookupDto { Id = d.CurrentHospitalId.Value, Name = hn }
                    : null
            }).ToList();

            return PaginationHelper.Create(mapped, total, request.PageNumber, request.PageSize);
        }

        public async Task<DoctorProfileDto> GetProfileAsync(int id)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id) ?? throw new Exception("Doctor not found");

            var history = await _historyRepo.GetByDoctorIdAsync(id);
            var educations = await _educationRepo.GetByDoctorIdAsync(id);
            var memberships = await _membershipRepo.GetByDoctorIdAsync(id);
            var specializations = await _specializationRepo.GetByDoctorIdAsync(id);
            var awards = await _awardRepo.GetByDoctorIdAsync(id);
            var publications = await _publicationRepo.GetByDoctorIdAsync(id);
            var fellowships = await _fellowshipRepo.GetByDoctorIdAsync(id);

            // Hospital lookup for both current + history
            var hospitalIds = new HashSet<int>();
            if (doctor.CurrentHospitalId.HasValue) hospitalIds.Add(doctor.CurrentHospitalId.Value);
            foreach (var h in history) hospitalIds.Add(h.HospitalId);

            var hospitalMap = await _lookupRepo.GetHospitalNamesByIdsAsync(hospitalIds.ToList());

            LookupDto? currentHospital = null;
            if (doctor.CurrentHospitalId.HasValue && hospitalMap.TryGetValue(doctor.CurrentHospitalId.Value, out var currentName))
            {
                currentHospital = new LookupDto { Id = doctor.CurrentHospitalId.Value, Name = currentName };
            }

            var profile = new DoctorProfileDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                ProfileOverview = doctor.ProfileOverview,
                TotalExperienceYears = doctor.TotalExperienceYears,

                CurrentHospital = currentHospital,
                CurrentDesignationName = doctor.CurrentDesignationName,

                HospitalHistory = history.Select(x => new DoctorHospitalHistoryDto
                {
                    Id = x.Id,
                    Hospital = new LookupDto
                    {
                        Id = x.HospitalId,
                        Name = hospitalMap.TryGetValue(x.HospitalId, out var hn) ? hn : "Unknown"
                    },
                    DesignationName = x.DesignationName,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    Notes = x.Notes
                }).ToList(),

                Educations = educations.Select(x => new DoctorEducationDto
                {
                    Id = x.Id,
                    Degree = x.Degree,
                    Institute = x.Institute,
                    FromYear = x.FromYear,
                    ToYear = x.ToYear
                }).ToList(),

                Memberships = memberships.Select(x => x.MembershipName).ToList(),
                Specializations = specializations.Select(x => x.SpecializationName).ToList(),

                Awards = awards.Select(x => new DoctorAwardDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Year = x.Year,
                    Issuer = x.Issuer
                }).ToList(),

                Publications = publications.Select(x => new DoctorPublicationDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Journal = x.Journal,
                    Year = x.Year,
                    Url = x.Url
                }).ToList(),

                Fellowships = fellowships.Select(x => new DoctorFellowshipDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Organization = x.Organization,
                    Country = x.Country,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate
                }).ToList()
            };

            return profile;
        }
    }
}
