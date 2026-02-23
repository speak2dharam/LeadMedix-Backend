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
    public class DoctorPublicationsController : ControllerBase
    {
        private readonly IDoctorPublicationService _service;
        public DoctorPublicationsController(IDoctorPublicationService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Add(int doctorId, [FromBody] CreateDoctorPublicationRequest dto)
        {
            var id = await _service.AddAsync(doctorId, dto);
            return Ok(ApiResponse<int>.SuccessResponse(id, "Publication added successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int doctorId, int id, [FromBody] UpdateDoctorPublicationRequest dto)
        {
            await _service.UpdateAsync(doctorId, id, dto);
            return Ok(ApiResponse<string>.SuccessResponse("OK", "Publication updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int doctorId, int id)
        {
            await _service.DeleteAsync(doctorId, id);
            return Ok(ApiResponse<string>.SuccessResponse("OK", "Publication deleted successfully"));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int doctorId)
        {
            var data = await _service.GetAsync(doctorId);
            return Ok(ApiResponse<object>.SuccessResponse(data));
        }
    }
}
