using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.LeadHospitalReviews.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LeadHospitalReviewsController : ControllerBase
    {
        private readonly ILeadHospitalReviewService _service;

        public LeadHospitalReviewsController(ILeadHospitalReviewService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<LeadHospitalReviewDto>>> Create([FromBody] CreateLeadHospitalReviewRequest request)
        {
            var data = await _service.CreateAsync(request);
            return Ok(ApiResponse<LeadHospitalReviewDto>.SuccessResponse(data, "Hospital review created."));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<LeadHospitalReviewDto>>> Update(int id, [FromBody] UpdateLeadHospitalReviewRequest request)
        {
            var data = await _service.UpdateAsync(id, request);
            return Ok(ApiResponse<LeadHospitalReviewDto>.SuccessResponse(data, "Hospital review updated."));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return Ok(ApiResponse<bool>.SuccessResponse(ok, "Hospital review deleted."));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<LeadHospitalReviewDto>>> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<LeadHospitalReviewDto>.SuccessResponse(data));
        }

        [HttpPost("paged")]
        public async Task<ActionResult<ApiResponse<PaginatedResponse<LeadHospitalReviewDto>>>> GetPaged([FromBody] PaginationRequest request)
        {
            var result = await _service.GetPagedAsync(request);
            return Ok(ApiResponse<PaginatedResponse<LeadHospitalReviewDto>>.SuccessResponse(result));
        }

        [HttpPost("lead/{leadId:int}/paged")]
        public async Task<ActionResult<ApiResponse<PaginatedResponse<LeadHospitalReviewDto>>>> GetPagedByLeadId(int leadId, [FromBody] PaginationRequest request)
        {
            var result = await _service.GetPagedByLeadIdAsync(leadId, request);
            return Ok(ApiResponse<PaginatedResponse<LeadHospitalReviewDto>>.SuccessResponse(result));
        }
    }
}
