using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadActivites.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/leads/{leadId:int}/activities")]
    [Authorize]
    [ApiController]
    public class LeadActivitiesController : ControllerBase
    {
        private readonly ILeadActivityService _service;

        public LeadActivitiesController(ILeadActivityService service)
        {
            _service = service;
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetPaged(int leadId, [FromBody] PaginationRequest request, [FromQuery] int? activityType = null)
        {
            var data = await _service.GetByLeadPagedAsync(leadId, request, activityType);
            return Ok(ApiResponse<PaginatedResponse<LeadActivityListItemDto>>.SuccessResponse(data));
        }

        [HttpPost]
        public async Task<IActionResult> Create(int leadId, [FromBody] LeadActivityCreateDto dto)
        {
            var id = await _service.AddManualAsync(leadId, dto);
            return Ok(ApiResponse<int>.SuccessResponse(id, "Activity added successfully"));
        }
    }
}
