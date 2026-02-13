using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IUserTokenRepository
    {
        Task AddAsync(UserToken token);
        Task<List<UserToken>> GetActiveTokensByUserIdAsync(int userId);
        Task RevokeAsync(UserToken token);
        Task<UserToken?> GetByTokenAsync(string token);
        Task<UserToken?> GetByRefreshTokenAsync(string refreshToken);
        Task<UserToken?> GetByAccessTokenAsync(string accessToken);
        Task UpdateAsync(UserToken token);
    }
}
