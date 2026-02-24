using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadAssignment.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LeadAssignmentHistoryController : ControllerBase
    {
        private readonly ILeadAssignmentHistoryService _service;

        public LeadAssignmentHistoryController(ILeadAssignmentHistoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLeadAssignmentHistoryDto dto)
        {
            var data = await _service.CreateAsync(dto);

            return Ok(ApiResponse<LeadAssignmentHistoryDto>.SuccessResponse(
                data,
                "Lead assignment history created successfully"
            ));
        }

        [HttpPost("paged/{leadId}")]
        public async Task<IActionResult> GetPaged(int leadId, [FromBody] PaginationRequest request)
        {
            var result = await _service.GetPagedByLeadIdAsync(leadId, request);

            // No ApiResponse wrapper for pagination (your choice)
            return Ok(result);
        }
    }
}
