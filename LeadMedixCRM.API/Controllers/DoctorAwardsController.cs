using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Features.Doctors.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/doctors/{doctorId}/[controller]")]
    [Authorize]
    [ApiController]
    public class DoctorAwardsController : ControllerBase
    {
        private readonly IDoctorAwardService _service;
        public DoctorAwardsController(IDoctorAwardService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Add(int doctorId, [FromBody] CreateDoctorAwardRequest dto)
        {
            var id = await _service.AddAsync(doctorId, dto);
            return Ok(ApiResponse<int>.SuccessResponse(id, "Award added successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int doctorId, int id, [FromBody] UpdateDoctorAwardRequest dto)
        {
            await _service.UpdateAsync(doctorId, id, dto);
            return Ok(ApiResponse<string>.SuccessResponse("OK", "Award updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int doctorId, int id)
        {
            await _service.DeleteAsync(doctorId, id);
            return Ok(ApiResponse<string>.SuccessResponse("OK", "Award deleted successfully"));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int doctorId)
        {
            var data = await _service.GetAsync(doctorId);
            return Ok(ApiResponse<object>.SuccessResponse(data));
        }
    }
}
