using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Domain.Entities;
using LeadMedixCRM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Infrastructure.Repositories
{
    public class UserTokenRepository:IUserTokenRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        public UserTokenRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public async Task AddAsync(UserToken token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            token.ExpiresAt = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"]));
            await _appDbContext.UserTokens.AddAsync(token);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<UserToken>> GetActiveTokensByUserIdAsync(int userId)
        {
            return await _appDbContext.UserTokens
            .Where(x => x.UserId == userId && !x.IsRevoked && x.IsActive)
            .ToListAsync();
        }

        public async Task<UserToken?> GetByTokenAsync(string token)
        {
            return await _appDbContext.UserTokens.FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task RevokeAsync(UserToken token)
        {
            token.IsRevoked = true;
            _appDbContext.UserTokens.Update(token);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
