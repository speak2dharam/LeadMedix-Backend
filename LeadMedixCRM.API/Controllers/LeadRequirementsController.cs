using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.LeadRequirements.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LeadRequirementsController : ControllerBase
    {
        private readonly ILeadRequirementService _service;

        public LeadRequirementsController(ILeadRequirementService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<LeadRequirementDto>>> Create([FromBody] CreateLeadRequirementRequest request)
        {
            var data = await _service.CreateAsync(request);
            return Ok(ApiResponse<LeadRequirementDto>.SuccessResponse(data, "Lead requirement created."));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<LeadRequirementDto>>> Update(int id, [FromBody] UpdateLeadRequirementRequest request)
        {
            var data = await _service.UpdateAsync(id, request);
            return Ok(ApiResponse<LeadRequirementDto>.SuccessResponse(data, "Lead requirement updated."));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return Ok(ApiResponse<bool>.SuccessResponse(ok, "Lead requirement deleted."));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<LeadRequirementDto>>> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<LeadRequirementDto>.SuccessResponse(data));
        }

        [HttpPost("paged")]
        public async Task<ActionResult<ApiResponse<PaginatedResponse<LeadRequirementDto>>>> GetPaged([FromBody] PaginationRequest request)
        {
            var result = await _service.GetPagedAsync(request);
            return Ok(ApiResponse<PaginatedResponse<LeadRequirementDto>>.SuccessResponse(result));
        }

        [HttpPost("lead/{leadId:int}/paged")]
        public async Task<ActionResult<ApiResponse<PaginatedResponse<LeadRequirementDto>>>> GetPagedByLeadId(int leadId, [FromBody] PaginationRequest request)
        {
            var result = await _service.GetPagedByLeadIdAsync(leadId, request);
            return Ok(ApiResponse<PaginatedResponse<LeadRequirementDto>>.SuccessResponse(result));
        }
    }
}
