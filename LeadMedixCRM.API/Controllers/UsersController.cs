using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Users.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        public UsersController(IUserService userService, ICurrentUserService currentUserService) {
            _userService = userService;
            _currentUserService = currentUserService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {

            await _userService.CreateAsync(dto, 1); // replace 1 with logged-in user id
            return Ok(ApiResponse<string>.SuccessResponse(null, "User created successfully"));
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var users = await _userService.GetAllAsync();

        //    return Ok(ApiResponse<List<UserResponseDto>>
        //        .SuccessResponse(users, "Users fetched successfully"));
        //}
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
        {
            var result = await _userService.GetPagedAsync(request);

            return Ok(ApiResponse<PaginatedResponse<UserResponseDto>>
                .SuccessResponse(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse(null, "User created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDto dto)
        {
            await _userService.UpdateAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            //return Ok();
            return Ok(ApiResponse<string>.SuccessResponse(null, "User deleted successfully"));
        }
    }
}
