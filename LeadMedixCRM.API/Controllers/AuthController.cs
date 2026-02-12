using Azure.Core;
using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Auth.Login.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
    }
}
