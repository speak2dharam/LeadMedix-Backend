using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Doctors.DTOs;
using LeadMedixCRM.Application.Features.Doctors.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorsController(IDoctorService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctorRequest dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(ApiResponse<int>.SuccessResponse(id, "Doctor added successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDoctorRequest dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok(ApiResponse<string>.SuccessResponse("OK", "Doctor updated"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse("OK", "Doctor deleted"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await _service.GetProfileAsync(id);
            return Ok(ApiResponse<DoctorProfileDto>.SuccessResponse(profile));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
        {
            var result = await _service.GetPagedAsync(request);
            return Ok(ApiResponse<PaginatedResponse<DoctorListItemDto>>.SuccessResponse(result));
        }
    }
}
