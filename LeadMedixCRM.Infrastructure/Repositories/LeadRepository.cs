using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.DTOs;
using LeadMedixCRM.Domain.Entities;
using LeadMedixCRM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Infrastructure.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private readonly AppDbContext _db;
        public LeadRepository(AppDbContext db) => _db = db;

        public Task<Lead?> GetByIdAsync(int id, CancellationToken ct = default)
            => _db.Leads.FirstOrDefaultAsync(x => x.Id == id, ct);

        public Task<Lead?> GetByPhoneNormalizedAsync(string phoneNormalized, CancellationToken ct = default)
            => _db.Leads.FirstOrDefaultAsync(x => x.PhoneNormalized == phoneNormalized, ct);

        public Task<Lead?> GetByEmailNormalizedAsync(string emailNormalized, CancellationToken ct = default)
            => _db.Leads.FirstOrDefaultAsync(x => x.EmailNormalized == emailNormalized, ct);

        public Task AddAsync(Lead lead, CancellationToken ct = default)
            => _db.Leads.AddAsync(lead, ct).AsTask();

        public async Task<PaginatedResponse<LeadResponseDto>> SearchAsync(LeadFilterRequest request, CancellationToken ct = default)
        {
            var q = _db.Leads.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var s = request.Search.Trim().ToLowerInvariant();
                var digits = new string(s.Where(char.IsDigit).ToArray());

                q = q.Where(x =>
                    x.FullName.ToLower().Contains(s) ||
                    x.PhoneNormalized.Contains(digits) ||
                    (x.EmailNormalized != null && x.EmailNormalized.Contains(s)));
            }

            if (request.Status.HasValue) q = q.Where(x => x.Status == request.Status.Value);
            if (request.Temperature.HasValue) q = q.Where(x => x.Temperature == request.Temperature.Value);
            if (request.SourceId.HasValue) q = q.Where(x => x.SourceId == request.SourceId.Value);
            if (request.CountryId.HasValue) q = q.Where(x => x.CountryId == request.CountryId.Value);
            if (request.TreatmentId.HasValue) q = q.Where(x => x.TreatmentId == request.TreatmentId.Value);
            if (request.AssignedToUserId.HasValue) q = q.Where(x => x.AssignedToUserId == request.AssignedToUserId.Value);

            q = q.OrderByDescending(x => x.CreatedAt);

            var totalRecords = await q.CountAsync(ct);

            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var items = await q
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new LeadResponseDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Phone = x.Phone,
                    Email = x.Email,
                    CountryId = x.CountryId,
                    TreatmentId = x.TreatmentId,
                    SourceId = x.SourceId,
                    Temperature = x.Temperature,
                    Status = x.Status,
                    AssignedToUserId = x.AssignedToUserId,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(ct);

            return new PaginatedResponse<LeadResponseDto>
            {
                Data = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };
        }
    }
}
