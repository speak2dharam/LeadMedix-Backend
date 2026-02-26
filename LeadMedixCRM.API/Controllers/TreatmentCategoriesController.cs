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
    public class TreatmentCategoriesController : ControllerBase
    {
        private readonly ITreatmentCategoryService _service;

        public TreatmentCategoriesController(ITreatmentCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = false)
        {
            var data = await _service.GetAllAsync(onlyActive);
            return Ok(ApiResponse<List<TreatmentCategoryListItemDto>>.SuccessResponse(data));
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetPaged([FromBody] PaginationRequest request, [FromQuery] string? search = null)
        {
            var data = await _service.GetPagedAsync(request, search);
            return Ok(ApiResponse<PaginatedResponse<TreatmentCategoryListItemDto>>.SuccessResponse(data));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TreatmentCategoryCreateDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(ApiResponse<int>.SuccessResponse(id, "Treatment category created successfully"));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TreatmentCategoryUpdateDto dto)
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
