using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Hospitals.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly IHospitalService _service;
        public HospitalsController(IHospitalService service) => _service = service;

        [Authorize(Policy = "MasterData.View")]
        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] PaginationRequest request)
        {
            var result = await _service.GetPagedAsync(request);
            return Ok(ApiResponse<PaginatedResponse<HospitalListItemDto>>.SuccessResponse(result));
        }

        [Authorize(Policy = "MasterData.View")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _service.GetByIdAsync(id);
            //return data == null ? NotFound() : Ok(data);
            if (data == null)
                return NotFound(ApiResponse<string>.FailureResponse("Hospital not found"));

            return Ok(ApiResponse<HospitalDetailDto>.SuccessResponse(data));
        }

        [Authorize(Policy = "MasterData.Edit")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HospitalUpsertRequest req)
        {
            var id = await _service.CreateAsync(req);
            return Ok(ApiResponse<int>.SuccessResponse(id, "Hospital created successfully"));
        }

        [Authorize(Policy = "MasterData.Edit")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] HospitalUpsertRequest req)
        {
            var ok = await _service.UpdateAsync(id, req);

            if (!ok)
                return NotFound(ApiResponse<string>.FailureResponse("Hospital not found"));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Hospital updated successfully"));
        }

        [Authorize(Policy = "MasterData.Edit")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);

            if (!ok)
                return NotFound(ApiResponse<string>.FailureResponse("Hospital not found"));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Hospital deleted successfully"));
        }


        [Authorize(Policy = "MasterData.Edit")]
        [HttpPost("{id:int}/logo")]
        public async Task<IActionResult> UploadLogo(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(ApiResponse<string>.FailureResponse("File missing"));

            await using var stream = file.OpenReadStream();
            var url = await _service.UploadLogoAsync(id, stream, file.FileName, file.ContentType);

            if (url == null)
                return NotFound(ApiResponse<string>.FailureResponse("Hospital not found"));

            return Ok(ApiResponse<string>.SuccessResponse(url, "Logo uploaded successfully"));
        }
    }
}
