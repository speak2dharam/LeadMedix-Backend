using Azure.Core;
using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Auth.Login.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUserService;
        public AuthController(IAuthService authService, ICurrentUserService currentUserService)
        {
            _authService = authService;
            _currentUserService = currentUserService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            //await _authService.LoginAsync(dto); // replace 1 with logged-in user id

            //return Ok(ApiResponse<string>.SuccessResponse(null, "User created successfully"));

            var result = await _authService.LoginAsync(dto);

            return Ok(ApiResponse<LoginResponseDto>
                .SuccessResponse(result, "Login successful"));
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = _currentUserService.Token;

            await _authService.LogoutAsync(token);

            return Ok(ApiResponse<string>
                .SuccessResponse(null, "Logged out successfully"));
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequestDto dto)
        {
            var result = await _authService.RefreshTokenAsync(dto.RefreshToken);

            return Ok(ApiResponse<LoginResponseDto>
                .SuccessResponse(result, "Token refreshed successfully"));
        }
    }
}
