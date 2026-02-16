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
            var result = await _authService.LoginAsync(dto);

            SetRefreshTokenCookie(result.RefreshToken);
            result.RefreshToken = null;

            return Ok(ApiResponse<LoginResponseDto>
                .SuccessResponse(result, "Login successful"));
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["lm_refresh"];

            if (!string.IsNullOrWhiteSpace(refreshToken))
                await _authService.LogoutAsync(refreshToken);

            Response.Cookies.Delete("lm_refresh", GetCookieOptionsForDelete());

            return Ok(ApiResponse<string>
                .SuccessResponse(null, "Logged out successfully"));
        }
        //[Authorize]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["lm_refresh"];

            if (string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized(ApiResponse<string>.FailureResponse("Missing refresh token"));

            var result = await _authService.RefreshTokenAsync(refreshToken);

            SetRefreshTokenCookie(result.RefreshToken);

            // Don't send refresh token back to frontend
            result.RefreshToken = null;

            return Ok(ApiResponse<LoginResponseDto>
                .SuccessResponse(result, "Token refreshed successfully"));
        }
        private void SetRefreshTokenCookie(string refreshToken)
        {
            var options = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,                 // HTTPS required in prod
                SameSite = SameSiteMode.None,  // use None if frontend/backend on different domains
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                Path = "/"
            };

            Response.Cookies.Append("lm_refresh", refreshToken, options);
        }

        private CookieOptions GetCookieOptionsForDelete()
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(-1),
                Path = "/"
            };
        }
    }
}
