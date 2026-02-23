using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Features.MasterData.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadMedixCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataService _service;
        public MasterDataController(IMasterDataService service) => _service = service;

        [Authorize(Policy = "MasterData.View")]
        [HttpGet("countries")]
        public async Task<IActionResult> Countries() => Ok(await _service.GetCountriesAsync());

        [Authorize(Policy = "MasterData.View")]
        [HttpGet("cities")]
        public async Task<IActionResult> Cities([FromQuery] int countryId)
            => Ok(await _service.GetCitiesByCountryAsync(countryId));

        [Authorize(Policy = "MasterData.Edit")]
        [HttpPost("countries")]
        public async Task<IActionResult> CreateCountry([FromBody] CountryDto dto)
            => Ok(new { id = await _service.CreateCountryAsync(dto) });

        [Authorize(Policy = "MasterData.Edit")]
        [HttpPut("countries/{id:int}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] CountryDto dto)
            => (await _service.UpdateCountryAsync(id, dto)) ? Ok() : NotFound();

        [Authorize(Policy = "MasterData.Edit")]
        [HttpDelete("countries/{id:int}")]
        public async Task<IActionResult> DeleteCountry(int id)
            => (await _service.DeleteCountryAsync(id)) ? Ok() : NotFound();

        [Authorize(Policy = "MasterData.Edit")]
        [HttpPost("cities")]
        public async Task<IActionResult> CreateCity([FromBody] CityDto dto)
            => Ok(new { id = await _service.CreateCityAsync(dto) });

        [Authorize(Policy = "MasterData.Edit")]
        [HttpPut("cities/{id:int}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] CityDto dto)
            => (await _service.UpdateCityAsync(id, dto)) ? Ok() : NotFound();

        [Authorize(Policy = "MasterData.Edit")]
        [HttpDelete("cities/{id:int}")]
        public async Task<IActionResult> DeleteCity(int id)
            => (await _service.DeleteCityAsync(id)) ? Ok() : NotFound();
        // ✅ GET: /api/MasterData/accreditations
        [HttpGet("accreditations")]
        public async Task<IActionResult> GetAccreditations()
        {
            var data = await _service.GetAccreditationsAsync();
            return Ok(ApiResponse<List<AccreditationDto>>.SuccessResponse(data, "Accreditations fetched successfully."));
        }

        // ✅ POST: /api/MasterData/accreditations
        [HttpPost("accreditations")]
        public async Task<IActionResult> CreateAccreditation([FromBody] AccreditationDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(ApiResponse<int>.FailureResponse("Name is required."));

            var id = await _service.CreateAccreditationAsync(dto);
            return Ok(ApiResponse<int>.SuccessResponse(id, "Accreditation created successfully."));
        }

        // ✅ PUT: /api/MasterData/accreditations/{id}
        [HttpPut("accreditations/{id:int}")]
        public async Task<IActionResult> UpdateAccreditation(int id, [FromBody] AccreditationDto dto)
        {
            if (id <= 0)
                return BadRequest(ApiResponse<bool>.FailureResponse("Invalid id."));

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(ApiResponse<bool>.FailureResponse("Name is required."));

            var ok = await _service.UpdateAccreditationAsync(id, dto);

            if (!ok)
                return NotFound(ApiResponse<bool>.FailureResponse("Accreditation not found."));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Accreditation updated successfully."));
        }

        // ✅ DELETE: /api/MasterData/accreditations/{id}
        [HttpDelete("accreditations/{id:int}")]
        public async Task<IActionResult> DeleteAccreditation(int id)
        {
            if (id <= 0)
                return BadRequest(ApiResponse<bool>.FailureResponse("Invalid id."));

            var ok = await _service.DeleteAccreditationAsync(id);

            if (!ok)
                return NotFound(ApiResponse<bool>.FailureResponse("Accreditation not found."));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Accreditation deleted successfully."));
        }
        [Authorize(Policy = "MasterData.Edit")]
        [HttpPost("accreditations/{id:int}/logo")]
        public async Task<IActionResult> UploadAccredationLogo(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(ApiResponse<string>.FailureResponse("File missing"));

            await using var stream = file.OpenReadStream();
            var url = await _service.UploadAccredationLogoAsync(id, stream, file.FileName, file.ContentType);

            if (url == null)
                return NotFound(ApiResponse<string>.FailureResponse("Hospital not found"));

            return Ok(ApiResponse<string>.SuccessResponse(url, "Logo uploaded successfully"));
        }
    }
}
