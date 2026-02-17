using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Features.Leads.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadService _service;

        public LeadsController(ILeadService service) => _service = service;

        [Authorize(Roles = "Admin,Coordinator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLeadDto dto, CancellationToken ct)
        {
            var result = await _service.CreateAsync(dto, ct);

            if (result.Duplicate != null)
            {
                return Conflict(ApiResponse<DuplicateLeadResponseDto>.FailureResponse(
                    result.Duplicate.Reason,
                    result.Duplicate
                ));
            }
            return Ok(ApiResponse<LeadResponseDto>.SuccessResponse(result.Created!, "Lead created successfully."));

        }

        [Authorize(Roles = "Admin,Coordinator")]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] LeadFilterRequest request, CancellationToken ct)
        {
            var data = await _service.SearchAsync(request, ct);
            return Ok(ApiResponse<object>.SuccessResponse(data, "Leads fetched successfully."));
        }

        [Authorize(Roles = "Admin,Coordinator")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var lead = await _service.GetByIdAsync(id, ct);
            return Ok(ApiResponse<LeadResponseDto>.SuccessResponse(lead, "Lead fetched successfully."));
        }

        [Authorize(Roles = "Admin,Coordinator")]
        [HttpPatch("{id:int}/assign")]
        public async Task<IActionResult> Assign(int id, [FromBody] AssignLeadDto dto, CancellationToken ct)
        {
            await _service.AssignAsync(id, dto, ct);
            return Ok(ApiResponse<string>.SuccessResponse("Lead assigned successfully.", "OK"));
        }

        [Authorize(Roles = "Admin,Coordinator")]
        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateLeadStatusDto dto, CancellationToken ct)
        {
            await _service.UpdateStatusAsync(id, dto, ct);
            return Ok(ApiResponse<string>.SuccessResponse("Lead status updated successfully.", "OK"));
        }

        [Authorize(Roles = "Admin,Coordinator")]
        [HttpPost("{id:int}/activities")]
        public async Task<IActionResult> AddActivity(int id, [FromBody] CreateLeadActivityDto dto, CancellationToken ct)
        {
            var activity = await _service.AddActivityAsync(id, dto, ct);
            return Ok(ApiResponse<LeadActivityResponseDto>.SuccessResponse(activity, "Activity added successfully."));
        }

        [Authorize(Roles = "Admin,Coordinator")]
        [HttpGet("{id:int}/activities")]
        public async Task<IActionResult> GetActivities(int id, CancellationToken ct)
        {
            var activities = await _service.GetActivitiesAsync(id, ct);
            return Ok(ApiResponse<List<LeadActivityResponseDto>>.SuccessResponse(activities, "Activities fetched successfully."));
        }
    }
}
