using EmployeeManagement.Application.DTOs.Master.City;
using EmployeeManagement.Application.Interfaces.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EmployeeManagement.API.Authorization.PermissionData;

namespace EmployeeManagement.API.Controllers.Master
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,HR,Employee")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [Authorize(Policy = PageList.City + "." + PageOpration.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _cityService.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        [Authorize(Policy = PageList.City + "." + PageOpration.View)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _cityService.GetByIdAsync(id, cancellationToken);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize(Policy = PageList.City + "." + PageOpration.Create)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCityDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _cityService.CreateAsync(dto, cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }


        [Authorize(Policy = PageList.City + "." + PageOpration.Update)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCityDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
                return BadRequest("Route id does not match request body.");

            var result = await _cityService.UpdateAsync(dto, cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
     
        [Authorize(Policy = PageList.City + "." + PageOpration.Delete)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _cityService.DeleteAsync(id, cancellationToken);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize(Policy = PageList.City + "." + PageOpration.Delete)]
        [HttpDelete("{id:guid}/deletepermanent")]
        public async Task<IActionResult> DeletePermanent(Guid id, CancellationToken cancellationToken)
        {
            var result = await _cityService.DeletePermanentAsync(id, cancellationToken);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize(Policy = PageList.City + "." + PageOpration.Restore)]
        [HttpPost("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
        {
            var result = await _cityService.RestoreAsync(id, cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Policy = PageList.City + "." + PageOpration.View)]
        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchCityDto dto, CancellationToken cancellationToken)
        {
            var result = await _cityService.SearchAsync(dto, cancellationToken);
            return Ok(result);
        }
    }
}
