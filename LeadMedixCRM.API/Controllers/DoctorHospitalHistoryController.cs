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
    public class DoctorHospitalHistoryController : ControllerBase
    {
        private readonly IDoctorHospitalHistoryService _service;
        public DoctorHospitalHistoryController(IDoctorHospitalHistoryService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Add(int doctorId, [FromBody] CreateDoctorHospitalHistoryRequest dto)
        {
            var id = await _service.AddAsync(doctorId, dto);
            return Ok(ApiResponse<int>.SuccessResponse(id, "Hospital history added"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int doctorId, int id, [FromBody] UpdateDoctorHospitalHistoryRequest dto)
        {
            await _service.UpdateAsync(doctorId, id, dto);
            return Ok(ApiResponse<string>.SuccessResponse("OK", "Hospital history updated"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int doctorId, int id)
        {
            await _service.DeleteAsync(doctorId, id);
            return Ok(ApiResponse<string>.SuccessResponse("OK", "Hospital history deleted"));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int doctorId)
        {
            var data = await _service.GetAsync(doctorId);
            return Ok(ApiResponse<object>.SuccessResponse(data));
        }
    }
}
