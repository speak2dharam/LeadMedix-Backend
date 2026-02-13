using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Exceptions;
using LeadMedixCRM.Application.Features.Auth.Login.DTOs;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Auth.Login
{
    public class AuthService:IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IUserTokenRepository _userTokenRepository;
        public AuthService(IUserRepository userRepository, ITokenService tokenService, IUserTokenRepository userTokenRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _userTokenRepository = userTokenRepository;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new NotFoundException("User does not exist with this email id");
            }
            else if (user.PasswordHash == dto.Password && user.IsActive == true)
            {
                //var accessExpiry = DateTime.UtcNow.AddMinutes(10);

                var refreshExpiry = DateTime.UtcNow.AddDays(7);
                //var refreshExpiry = DateTime.UtcNow.AddMinutes(2);
                var accessToken = _tokenService.GenerateToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();

                var userToken = new UserToken
                {
                    UserId = user.Id,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    //AccessTokenExpiresAt = accessExpiry,
                    RefreshTokenExpiresAt = refreshExpiry,
                    IsRevoked = false
                };

                await _userTokenRepository.AddAsync(userToken);

                return new LoginResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    UserId = user.Id,
                    Email = user.Email,
                    UserRole = user.RoleId
                };
            }
            else 
            {
                throw new NotFoundException("Invalid login details");
            }
        }

        public async Task LogoutAsync(string token)
        {
            var userToken = await _userTokenRepository.GetByTokenAsync(token);

            if (userToken == null)
                throw new NotFoundException("Token not found");

            userToken.IsRevoked = true;

            await _userTokenRepository.RevokeAsync(userToken);
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var tokenEntity = await _userTokenRepository.GetByRefreshTokenAsync(refreshToken);

            if (tokenEntity == null || tokenEntity.IsRevoked)
                throw new ValidationException("Invalid refresh token");

            if (tokenEntity.RefreshTokenExpiresAt < DateTime.UtcNow)
                throw new ValidationException("Refresh token expired");

            var user = await _userRepository.GetByIdAsync(tokenEntity.UserId);

            //var newAccessExpiry = DateTime.UtcNow.AddMinutes(15);
            var newRefreshExpiry = DateTime.UtcNow.AddDays(7);

            var newAccessToken = _tokenService.GenerateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // 🔥 ROTATION
            tokenEntity.AccessToken = newAccessToken;
            tokenEntity.RefreshToken = newRefreshToken;
            //tokenEntity.AccessTokenExpiresAt = newAccessExpiry;
            tokenEntity.RefreshTokenExpiresAt = newRefreshExpiry;

            await _userTokenRepository.UpdateAsync(tokenEntity);

            return new LoginResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                UserId = user.Id,
                Email = user.Email,
                UserRole = user.RoleId
            };
        }
    }
}
