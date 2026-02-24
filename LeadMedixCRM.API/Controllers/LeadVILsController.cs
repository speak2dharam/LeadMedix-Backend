using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadVILs.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LeadVILsController : ControllerBase
    {
        private readonly ILeadVILService _service;

        public LeadVILsController(ILeadVILService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<LeadVILDto>>> Create([FromBody] CreateLeadVILRequest request)
        {
            var data = await _service.CreateAsync(request);
            return Ok(ApiResponse<LeadVILDto>.SuccessResponse(data, "VIL entry created."));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<LeadVILDto>>> Update(int id, [FromBody] UpdateLeadVILRequest request)
        {
            var data = await _service.UpdateAsync(id, request);
            return Ok(ApiResponse<LeadVILDto>.SuccessResponse(data, "VIL entry updated."));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return Ok(ApiResponse<bool>.SuccessResponse(ok, "VIL entry deleted."));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<LeadVILDto>>> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<LeadVILDto>.SuccessResponse(data));
        }

        [HttpPost("paged")]
        public async Task<ActionResult<ApiResponse<PaginatedResponse<LeadVILDto>>>> GetPaged([FromBody] PaginationRequest request)
        {
            var result = await _service.GetPagedAsync(request);
            return Ok(ApiResponse<PaginatedResponse<LeadVILDto>>.SuccessResponse(result));
        }

        [HttpPost("lead/{leadId:int}/paged")]
        public async Task<ActionResult<ApiResponse<PaginatedResponse<LeadVILDto>>>> GetPagedByLeadId(int leadId, [FromBody] PaginationRequest request)
        {
            var result = await _service.GetPagedByLeadIdAsync(leadId, request);
            return Ok(ApiResponse<PaginatedResponse<LeadVILDto>>.SuccessResponse(result));
        }
    }
}
