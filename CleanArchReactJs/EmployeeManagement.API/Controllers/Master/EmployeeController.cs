using Asp.Versioning;
using EmployeeManagement.Application.DTOs.Master.Employee;
using EmployeeManagement.Application.Interfaces.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EmployeeManagement.API.Authorization.PermissionData;

namespace EmployeeManagement.API.Controllers.Master
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [ApiController]
    [Route("api/[controller]")]
  //  [Authorize(Roles = "Admin,HR,Employee")]
    [ApiVersion(1.0)]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
       // [Authorize(Policy = PageList.Employee + "." + PageOpration.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _employeeService.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        //[Authorize(Policy = PageList.Employee + "." + PageOpration.View)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _employeeService.GetByIdAsync(id, cancellationToken);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        //[Authorize(Employees = "Admin,HR")]
        //[Authorize(Policy = Employees.EmployeeCreate)]
        [Authorize(Policy = PageList.Employee + "." + PageOpration.Create)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _employeeService.CreateAsync(dto, cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        //[Authorize(Employees = "Admin,HR")]
        [Authorize(Policy = PageList.Employee + "." + PageOpration.Update)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
                return BadRequest("Route id does not match request body.");

            var result = await _employeeService.UpdateAsync(dto, cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        //[Authorize(Employees = "Admin")]
        [Authorize(Policy = PageList.Employee + "." + PageOpration.Delete)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _employeeService.DeleteAsync(id, cancellationToken);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize(Policy = PageList.Employee + "." + PageOpration.Delete)]
        [HttpDelete("{id:guid}/deletepermanent")]
        public async Task<IActionResult> DeletePermanent(Guid id, CancellationToken cancellationToken)
        {
            var result = await _employeeService.DeletePermanentAsync(id, cancellationToken);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // [Authorize(Employees = "Admin")]
        [Authorize(Policy = PageList.Employee + "." + PageOpration.Restore)]
        [HttpPost("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
        {
            var result = await _employeeService.RestoreAsync(id, cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Policy = PageList.Employee + "." + PageOpration.View)]
        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchEmployeeDto dto, CancellationToken cancellationToken)
        {
            var result = await _employeeService.SearchAsync(dto, cancellationToken);
            return Ok(result);
        }
        [HttpGet("dummyData")]
        public async Task<IActionResult> DummyData()
        {
            await _employeeService.CreateDummyData();

            return Ok();

        }
    }

}
