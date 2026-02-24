using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadAssignment.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Leads.LeadAssignment
{
    public class LeadAssignmentHistoryService : ILeadAssignmentHistoryService
    {
        private readonly ILeadAssignmentHistoryRepository _repo;
        private readonly ICurrentUserService _currentUser;

        public LeadAssignmentHistoryService(
            ILeadAssignmentHistoryRepository repo,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        public async Task<LeadAssignmentHistoryDto> CreateAsync(CreateLeadAssignmentHistoryDto dto)
        {
            var entity = new LeadAssignmentHistory
            {
                LeadId = dto.LeadId,
                FromUserId = dto.FromUserId,
                ToUserId = dto.ToUserId,
                Reason = dto.Reason,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId ?? 0
            };

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return new LeadAssignmentHistoryDto
            {
                Id = entity.Id,
                LeadId = entity.LeadId,
                FromUserId = entity.FromUserId,
                ToUserId = entity.ToUserId,
                Reason = entity.Reason,
                CreatedAt = entity.CreatedAt
            };
        }

        public async Task<PaginatedResponse<LeadAssignmentHistoryDto>> GetPagedByLeadIdAsync(
            int leadId,
            PaginationRequest request)
        {
            return await _repo.GetPagedByLeadIdAsync(leadId, request);
        }
    }
}
