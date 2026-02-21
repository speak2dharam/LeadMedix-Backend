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
    }
}
