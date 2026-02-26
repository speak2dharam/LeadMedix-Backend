using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Security;
using LeadMedixCRM.Application.Features.Leads.Leads.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadService _service;
        public LeadsController(ILeadService service) => _service = service;

        [HttpPost]
        [Authorize(Policy = Policies.LeadCreate)]
        public async Task<IActionResult> Create([FromBody] LeadCreateDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(ApiResponse<int>.SuccessResponse(id, "Lead created successfully"));
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = Policies.LeadEdit)]
        public async Task<IActionResult> Update(int id, [FromBody] LeadUpdateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Lead updated successfully"));
        }

        [HttpGet("{id:int}")]
        [Authorize] // view rules enforced in service (assigned-only for coordinator/groundstaff)
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<object>.SuccessResponse(data, "Lead fetched successfully"));
        }

        [HttpPost("paged")]
        [Authorize]
        public async Task<IActionResult> GetPaged([FromBody] LeadFilterRequest request)
        {
            var data = await _service.GetPagedAsync(request);
            return Ok(ApiResponse<object>.SuccessResponse(data, "Leads fetched successfully"));
        }

        [HttpPost("{id:int}/assign")]
        [Authorize(Policy = Policies.LeadAssign)]
        public async Task<IActionResult> Assign(int id, [FromBody] LeadAssignDto dto)
        {
            await _service.AssignAsync(id, dto);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Lead assigned successfully"));
        }

        [HttpPost("{id:int}/status")]
        [Authorize(Policy = Policies.LeadUpdateStatus)]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] LeadStatusUpdateDto dto)
        {
            await _service.UpdateStatusAsync(id, dto);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Lead status updated successfully"));
        }

        [HttpPost("{id:int}/discard")]
        [Authorize(Policy = Policies.LeadDiscard)]
        public async Task<IActionResult> Discard(int id, [FromBody] LeadDiscardDto dto)
        {
            await _service.DiscardAsync(id, dto);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Lead discarded successfully"));
        }

        [HttpPost("{id:int}/restore")]
        [Authorize(Policy = Policies.LeadRestore)]
        public async Task<IActionResult> Restore(int id)
        {
            await _service.RestoreDiscardedAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Lead restored successfully"));
        }

        [HttpPost("{id:int}/close")]
        [Authorize(Policy = Policies.LeadClose)]
        public async Task<IActionResult> Close(int id, [FromBody] LeadCloseDto dto)
        {
            await _service.CloseAsync(id, dto);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Lead closed successfully"));
        }

        [HttpPost("{id:int}/reopen")]
        [Authorize(Policy = Policies.LeadReopen)]
        public async Task<IActionResult> Reopen(int id)
        {
            await _service.ReopenAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Lead reopened successfully"));
        }
    }
}
