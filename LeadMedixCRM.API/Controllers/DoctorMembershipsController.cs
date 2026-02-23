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
    public class DoctorMembershipsController : ControllerBase
    {
        private readonly IDoctorMembershipService _service;
        public DoctorMembershipsController(IDoctorMembershipService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Add(int doctorId, [FromBody] CreateDoctorMembershipRequest dto)
        {
            var id = await _service.AddAsync(doctorId, dto);
            return Ok(ApiResponse<int>.SuccessResponse(id, "Membership added successfully"));
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int doctorId, int id, [FromBody] updatedocto dto)
        //{
        //    await _service.UpdateAsync(doctorId, id, dto);
        //    return Ok(ApiResponse<string>.SuccessResponse("OK", "Membership updated successfully"));
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int doctorId, int id)
        {
            await _service.DeleteAsync(doctorId, id);
            return Ok(ApiResponse<string>.SuccessResponse("OK", "Membership deleted successfully"));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int doctorId)
        {
            var data = await _service.GetAsync(doctorId);
            return Ok(ApiResponse<object>.SuccessResponse(data));
        }
    }
}
