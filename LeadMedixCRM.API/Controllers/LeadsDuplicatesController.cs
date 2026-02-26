using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.Duplicates.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/Leads/duplicates")]
    [Authorize]
    [ApiController]
    public class LeadsDuplicatesController : ControllerBase
    {
        private readonly ILeadDuplicateService _service;
        public LeadsDuplicatesController(ILeadDuplicateService service) => _service = service;

        // POST /api/Leads/duplicates/paged
        [HttpPost("paged")]
        public async Task<IActionResult> GetGroups([FromBody] PaginationRequest request)
        {
            var data = await _service.GetDuplicateGroupsPagedAsync(request);
            return Ok(ApiResponse<object>.SuccessResponse(data, "Duplicate groups fetched successfully"));
        }

        // GET /api/Leads/duplicates/{parentLeadId}
        [HttpGet("{parentLeadId:int}")]
        public async Task<IActionResult> GetGroupDetails(int parentLeadId)
        {
            var data = await _service.GetDuplicateGroupDetailsAsync(parentLeadId);
            return Ok(ApiResponse<object>.SuccessResponse(data, "Duplicate lead list fetched successfully"));
        }

        // POST /api/Leads/duplicates/unlink/{duplicateLeadId}
        [HttpPost("unlink/{duplicateLeadId:int}")]
        public async Task<IActionResult> Unlink(int duplicateLeadId, [FromQuery] string? reason = null)
        {
            await _service.UnlinkDuplicateAsync(duplicateLeadId, reason);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Duplicate unlinked successfully"));
        }

        // POST /api/Leads/duplicates/merge/{parentLeadId}
        [HttpPost("merge/{parentLeadId:int}")]
        public async Task<IActionResult> Merge(int parentLeadId, [FromBody] MergeDuplicatesRequest request)
        {
            await _service.MergeDuplicatesAsync(parentLeadId, request);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Duplicates merged successfully"));
        }
    }
}
