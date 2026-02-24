using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Leads.LeadQuote.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LeadQuotationController : ControllerBase
    {
        private readonly ILeadQuotationService _service;

        public LeadQuotationController(ILeadQuotationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLeadQuotationDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return Ok(ApiResponse<LeadQuotationDto>.SuccessResponse(data, "Quotation created"));
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateQuotationStatusDto dto)
        {
            var data = await _service.UpdateStatusAsync(dto);
            if (data == null)
                return Ok(ApiResponse<object>.FailureResponse("Quotation not found"));

            return Ok(ApiResponse<LeadQuotationDto>.SuccessResponse(data, "Quotation status updated"));
        }

        [HttpPost("paged/{leadId}")]
        public async Task<IActionResult> GetPaged(int leadId, [FromBody] PaginationRequest request)
        {
            var result = await _service.GetPagedByLeadIdAsync(leadId, request);
            return Ok(result);
        }
    }
}
