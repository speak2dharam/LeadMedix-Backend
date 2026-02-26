using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Treatments.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TreatmentsController : ControllerBase
    {
        private readonly ITreatmentService _service;

        public TreatmentsController(ITreatmentService service)
        {
            _service = service;
        }

        [HttpGet("by-category/{categoryId:int}")]
        public async Task<IActionResult> GetByCategory(int categoryId, [FromQuery] bool onlyActive = false)
        {
            var data = await _service.GetByCategoryAsync(categoryId, onlyActive);
            return Ok(ApiResponse<List<TreatmentListItemDto>>.SuccessResponse(data));
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetPaged([FromBody] PaginationRequest request, [FromQuery] int? categoryId = null, [FromQuery] string? search = null)
        {
            var data = await _service.GetPagedAsync(request, categoryId, search);
            return Ok(ApiResponse<PaginatedResponse<TreatmentListItemDto>>.SuccessResponse(data));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TreatmentCreateDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(ApiResponse<int>.SuccessResponse(id, "Treatment created successfully"));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TreatmentUpdateDto dto)
        {
            var msg = await _service.UpdateAsync(id, dto);
            return Ok(ApiResponse<string>.SuccessResponse(null, msg));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var msg = await _service.DeleteAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse(null, msg));
        }
    }
}
