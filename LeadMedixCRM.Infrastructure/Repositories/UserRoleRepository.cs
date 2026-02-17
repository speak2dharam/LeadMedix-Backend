using LeadMedixCRM.Application.Common.Interfaces.Repositories;
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
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;

        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetRoleCodesByUserIdAsync(int userId)
        {
            // No FK constraints: join manually via IDs
            var roleIds = await _context.UserRoles
                .Where(ur => ur.UserId == userId && !ur.IsDeleted)
                .Select(ur => ur.RoleId)
                .ToListAsync();

            var roleCodes = await _context.Roles
                .Where(r => roleIds.Contains(r.Id) && !r.IsDeleted)
                .Select(r => r.Code)
                .ToListAsync();

            return roleCodes;
        }

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            var exists = await _context.UserRoles.AnyAsync(x =>
                x.UserId == userId && x.RoleId == roleId && !x.IsDeleted);

            if (exists) return;

            await _context.UserRoles.AddAsync(new UserRole
            {
                UserId = userId,
                RoleId = roleId
            });

            await _context.SaveChangesAsync();
        }

        public async Task RemoveRoleAsync(int userId, int roleId)
        {
            var ur = await _context.UserRoles.FirstOrDefaultAsync(x =>
                x.UserId == userId && x.RoleId == roleId && !x.IsDeleted);

            if (ur == null) return;

            ur.IsDeleted = true;
            ur.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
