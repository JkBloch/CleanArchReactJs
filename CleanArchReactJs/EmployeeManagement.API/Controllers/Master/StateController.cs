using EmployeeManagement.Application.DTOs.Master.State;
using EmployeeManagement.Application.Interfaces.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EmployeeManagement.API.Authorization.PermissionData;

namespace EmployeeManagement.API.Controllers.Master
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin,HR,Employee")]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;
        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }
        [Authorize(Policy = PageList.State +"." + PageOpration.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _stateService.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        [Authorize(Policy = PageList.State + "." + PageOpration.View)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _stateService.GetByIdAsync(id, cancellationToken);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        //[Authorize(States = "Admin,HR")]
        //[Authorize(Policy = States.StateCreate)]
        [Authorize(Policy = PageList.State + "." + PageOpration.Create)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStateDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _stateService.CreateAsync(dto, cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        //[Authorize(States = "Admin,HR")]
        [Authorize(Policy = PageList.State + "." + PageOpration.Update)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStateDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
                return BadRequest("Route id does not match request body.");

            var result = await _stateService.UpdateAsync(dto, cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        //[Authorize(States = "Admin")]
        [Authorize(Policy = PageList.State + "." + PageOpration.Delete)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _stateService.DeleteAsync(id, cancellationToken);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize(Policy = PageList.State + "." + PageOpration.Delete)]
        [HttpDelete("{id:guid}/deletepermanent")]
        public async Task<IActionResult> DeletePermanent(Guid id, CancellationToken cancellationToken)
        {
            var result = await _stateService.DeletePermanentAsync(id, cancellationToken);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // [Authorize(States = "Admin")]
        [Authorize(Policy = PageList.State + "." + PageOpration.Restore)]
        [HttpPost("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
        {
            var result = await _stateService.RestoreAsync(id, cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Policy = PageList.State + "." + PageOpration.View)]
        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchStateDto dto, CancellationToken cancellationToken)
        {
            var result = await _stateService.SearchAsync(dto, cancellationToken);
            return Ok(result);
        }
    }
}
