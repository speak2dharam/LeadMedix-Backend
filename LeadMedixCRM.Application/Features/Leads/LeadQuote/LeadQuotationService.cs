using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadQuote.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using LeadMedixCRM.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.LeadQuote
{
    public class LeadQuotationService : ILeadQuotationService
    {
        private readonly ILeadQuotationRepository _repo;
        private readonly IMasterRepository<QuotationStatusMaster> _statusRepo;
        private readonly ICurrentUserService _currentUser;

        public LeadQuotationService(
            ILeadQuotationRepository repo,
            IMasterRepository<QuotationStatusMaster> statusRepo,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _statusRepo = statusRepo;
            _currentUser = currentUser;
        }

        public async Task<LeadQuotationDto> CreateAsync(CreateLeadQuotationDto dto)
        {
            // validate status exists (and active, if you want)
            var status = await _statusRepo.GetByIdAsync(dto.QuotationStatusId);
            if (status == null || status.IsDeleted)
                throw new ValidationException($"Invalid quotation status id: {dto.QuotationStatusId}");

            var entity = new LeadQuotation
            {
                LeadId = dto.LeadId,
                HospitalId = dto.HospitalId,
                QuotationStatusId = dto.QuotationStatusId,

                Amount = dto.Amount,
                Currency = dto.Currency,
                ValidTill = dto.ValidTill,
                Inclusions = dto.Inclusions,
                Exclusions = dto.Exclusions,

                // timeline based on status code
                RequestedAt = status.Code.Equals("REQUESTED", StringComparison.OrdinalIgnoreCase)
                    ? DateTime.UtcNow
                    : null,

                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId
            };

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return MapToDto(entity);
        }

        public async Task<LeadQuotationDto?> UpdateStatusAsync(UpdateQuotationStatusDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id);
            if (entity == null) return null;

            var status = await _statusRepo.GetByIdAsync(dto.QuotationStatusId);
            if (status == null || status.IsDeleted)
                throw new ValidationException($"Invalid quotation status id: {dto.QuotationStatusId}");

            entity.QuotationStatusId = dto.QuotationStatusId;

            ApplyTimeline(entity, status.Code);

            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = _currentUser.UserId;

            await _repo.UpdateAsync(entity);
            await _repo.SaveChangesAsync();

            return MapToDto(entity);
        }

        public Task<PaginatedResponse<LeadQuotationDto>> GetPagedByLeadIdAsync(int leadId, PaginationRequest request)
            => _repo.GetPagedByLeadIdAsync(leadId, request);

        private static void ApplyTimeline(LeadQuotation entity, string code)
        {
            // only set if not already set (important)
            if (code.Equals("REQUESTED", StringComparison.OrdinalIgnoreCase))
                entity.RequestedAt ??= DateTime.UtcNow;

            if (code.Equals("RECEIVED", StringComparison.OrdinalIgnoreCase))
                entity.ReceivedAt ??= DateTime.UtcNow;

            if (code.Equals("SHARED", StringComparison.OrdinalIgnoreCase))
                entity.SharedAt ??= DateTime.UtcNow;

            // optionally: for EXPIRED, FINALIZED etc, you might add columns later
        }

        private static LeadQuotationDto MapToDto(LeadQuotation entity)
        {
            return new LeadQuotationDto
            {
                Id = entity.Id,
                LeadId = entity.LeadId,
                HospitalId = entity.HospitalId,
                QuotationStatusId = entity.QuotationStatusId,

                Amount = entity.Amount,
                Currency = entity.Currency,
                ValidTill = entity.ValidTill,
                Inclusions = entity.Inclusions,
                Exclusions = entity.Exclusions,

                RequestedAt = entity.RequestedAt,
                ReceivedAt = entity.ReceivedAt,
                SharedAt = entity.SharedAt,

                CreatedAt = entity.CreatedAt
            };
        }
    }
}
