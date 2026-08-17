using Asp.Versioning;
using EmployeeManagement.Application.DTOs.Master.Department;
using EmployeeManagement.Application.Interfaces.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EmployeeManagement.API.Authorization.PermissionData;

namespace EmployeeManagement.API.Controllers.Master
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [ApiController]
    //[Route("api/[controller]")]
    [Authorize(Roles = "Admin,HR,Employee")]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [Authorize(Policy = PageList.Department + "." + PageOpration.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _departmentService.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        [Authorize(Policy = PageList.Department + "." + PageOpration.View)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _departmentService.GetByIdAsync(id, cancellationToken);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        //[Authorize(Departments = "Admin,HR")]
        //[Authorize(Policy = Departments.DepartmentCreate)]
        [Authorize(Policy = PageList.Department + "." + PageOpration.Create)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _departmentService.CreateAsync(dto, cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        //[Authorize(Departments = "Admin,HR")]
        [Authorize(Policy = PageList.Department + "." + PageOpration.Update)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDepartmentDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
                return BadRequest("Route id does not match request body.");

            var result = await _departmentService.UpdateAsync(dto, cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        //[Authorize(Departments = "Admin")]
        [Authorize(Policy = PageList.Department + "." + PageOpration.Delete)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _departmentService.DeleteAsync(id, cancellationToken);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize(Policy = PageList.Department + "." + PageOpration.Delete)]
        [HttpDelete("{id:guid}/deletepermanent")]
        public async Task<IActionResult> DeletePermanent(Guid id, CancellationToken cancellationToken)
        {
            var result = await _departmentService.DeletePermanentAsync(id, cancellationToken);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // [Authorize(Departments = "Admin")]
        [Authorize(Policy = PageList.Department + "." + PageOpration.Restore)]
        [HttpPost("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
        {
            var result = await _departmentService.RestoreAsync(id, cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Policy = PageList.Department + "." + PageOpration.View)]
        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchDepartmentDto dto, CancellationToken cancellationToken)
        {
            var result = await _departmentService.SearchAsync(dto, cancellationToken);
            return Ok(result);
        }
    }
}
