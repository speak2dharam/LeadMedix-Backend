using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Features.Leads.Leads.DTOs;
using LeadMedixCRM.Domain.Entities.Leads;
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
        private readonly AppDbContext _context;

        public LeadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Lead?> GetByIdAsync(int id)
        {
            return await _context.Leads.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(Lead lead) => await _context.Leads.AddAsync(lead);
        public Task UpdateAsync(Lead lead)
        {
            _context.Leads.Update(lead);
            return Task.CompletedTask;
        }

        public async Task<(List<Lead> Items, int TotalRecords)> GetPagedAsync(LeadFilterRequest request, int? forceAssignedToUserId = null)
        {
            var q = _context.Leads.AsQueryable();

            // coordinator / groundstaff view
            if (forceAssignedToUserId.HasValue)
                q = q.Where(x => x.AssignedToUserId == forceAssignedToUserId.Value);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var s = request.Search.Trim().ToLower();
                q = q.Where(x =>
                    x.FullName.ToLower().Contains(s) ||
                    (x.PhoneNormalized != null && x.PhoneNormalized.Contains(s)) ||
                    (x.EmailNormalized != null && x.EmailNormalized.Contains(s)) ||
                    (x.Phone != null && x.Phone.Contains(s)) ||
                    (x.Email != null && x.Email.ToLower().Contains(s))
                );
            }

            if (request.Status.HasValue) q = q.Where(x => x.Status == request.Status.Value);
            if (request.Temperature.HasValue) q = q.Where(x => x.Temperature == request.Temperature.Value);
            if (request.AssignedToUserId.HasValue) q = q.Where(x => x.AssignedToUserId == request.AssignedToUserId.Value);
            if (request.LeadSourceId.HasValue) q = q.Where(x => x.LeadSourceId == request.LeadSourceId.Value);
            if (request.TreatmentCategoryId.HasValue) q = q.Where(x => x.TreatmentCategoryId == request.TreatmentCategoryId.Value);
            if (request.TreatmentId.HasValue) q = q.Where(x => x.TreatmentId == request.TreatmentId.Value);

            if (request.IsClosed.HasValue) q = q.Where(x => x.IsClosed == request.IsClosed.Value);
            if (request.IsDiscarded.HasValue) q = q.Where(x => x.IsDiscarded == request.IsDiscarded.Value);

            q = q.OrderByDescending(x => x.LastActivityAt ?? x.CreatedAt);

            var total = await q.CountAsync();

            var page = request.PageNumber < 1 ? 1 : request.PageNumber;
            var size = request.PageSize < 1 ? 10 : request.PageSize;

            var items = await q.Skip((page - 1) * size).Take(size).ToListAsync();
            return (items, total);
        }

        public Task<bool> PhoneExistsAsync(string phoneNormalized, int? excludeLeadId = null)
        {
            var q = _context.Leads.Where(x => x.PhoneNormalized == phoneNormalized);
            if (excludeLeadId.HasValue) q = q.Where(x => x.Id != excludeLeadId.Value);
            return q.AnyAsync();
        }

        public Task<bool> EmailExistsAsync(string emailNormalized, int? excludeLeadId = null)
        {
            var q = _context.Leads.Where(x => x.EmailNormalized == emailNormalized);
            if (excludeLeadId.HasValue) q = q.Where(x => x.Id != excludeLeadId.Value);
            return q.AnyAsync();
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
        public async Task<Lead?> FindDuplicateAsync(string? phoneNormalized, string? emailNormalized)
        {
            if (string.IsNullOrWhiteSpace(phoneNormalized) && string.IsNullOrWhiteSpace(emailNormalized))
                return null;

            var q = _context.Leads.AsQueryable();

            q = q.Where(x => !x.IsDeleted);

            // optional: ignore merged leads as source
            q = q.Where(x => !x.IsMerged);

            q = q.Where(x =>
                (!string.IsNullOrWhiteSpace(phoneNormalized) && x.PhoneNormalized == phoneNormalized) ||
                (!string.IsNullOrWhiteSpace(emailNormalized) && x.EmailNormalized == emailNormalized)
            );

            return await q.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        }
        public async Task<List<Lead>> GetDuplicatesByParentIdAsync(int parentLeadId)
        {
            return await _context.Leads
                .Where(x => x.DuplicateOfLeadId == parentLeadId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
