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
                var getToken = _tokenService.GenerateToken(user);
                //return _tokenService.GenerateToken(user);
                var userToken = new UserToken
                {
                    UserId = user.Id,
                    Token = getToken,
                    CreatedByIp = ""
                };

                await _userTokenRepository.AddAsync(userToken);

                return new LoginResponseDto { 
                    Token = getToken,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    UserId = user.Id,
                    Email = user.Email,
                    UserRole = user.RoleId,
                    profilePic = ""

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
    }
}
