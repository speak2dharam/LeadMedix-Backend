using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Features.LeadMasters.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LeadMastersController : ControllerBase
    {
        private readonly ILeadMastersService _service;

        public LeadMastersController(ILeadMastersService service)
        {
            _service = service;
        }

        // GET: /api/LeadMasters/{masterKey}?activeOnly=true
        [HttpGet("{masterKey}")]
        public async Task<ActionResult<ApiResponse<List<MasterDto>>>> Get(string masterKey, [FromQuery] bool activeOnly = true)
        {
            var data = await _service.GetAsync(masterKey, activeOnly);
            return Ok(ApiResponse<List<MasterDto>>.SuccessResponse(data));
        }

        // POST: /api/LeadMasters/{masterKey}
        [HttpPost("{masterKey}")]
        public async Task<ActionResult<ApiResponse<MasterDto>>> Create(string masterKey, [FromBody] UpsertMasterRequest request)
        {
            var data = await _service.CreateAsync(masterKey, request);
            return Ok(ApiResponse<MasterDto>.SuccessResponse(data, "Created."));
        }

        // PUT: /api/LeadMasters/{masterKey}/{id}
        [HttpPut("{masterKey}/{id:int}")]
        public async Task<ActionResult<ApiResponse<MasterDto>>> Update(string masterKey, int id, [FromBody] UpsertMasterRequest request)
        {
            var data = await _service.UpdateAsync(masterKey, id, request);
            return Ok(ApiResponse<MasterDto>.SuccessResponse(data, "Updated."));
        }

        // DELETE: /api/LeadMasters/{masterKey}/{id}
        [HttpDelete("{masterKey}/{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(string masterKey, int id)
        {
            var ok = await _service.DeleteAsync(masterKey, id);
            return Ok(ApiResponse<bool>.SuccessResponse(ok, "Deleted."));
        }
    }
}
