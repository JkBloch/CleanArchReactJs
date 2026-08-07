using EmployeeManagement.Application.DTOs.RolePermissions;
using EmployeeManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;
        public RolePermissionController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _rolePermissionService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _rolePermissionService.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        //[Authorize(RolePermissions = "Admin,HR")]
        //[Authorize(Policy = RolePermissions.RolePermissionCreate)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRolePermissionDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _rolePermissionService.CreateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        //[Authorize(RolePermissions = "Admin,HR")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRolePermissionDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Route id does not match request body.");

            var result = await _rolePermissionService.UpdateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        //[Authorize(RolePermissions = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _rolePermissionService.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        [HttpDelete("{id:guid}/deletepermanent")]
        public async Task<IActionResult> DeletePermanent(Guid id)
        {
            var result = await _rolePermissionService.DeletePermanentAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // [Authorize(RolePermissions = "Admin")]
        [HttpPost("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id)
        {
            var result = await _rolePermissionService.RestoreAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchRolePermissionDto dto)
        {
            var result = await _rolePermissionService.SearchAsync(dto);
            return Ok(result);
        }
    }
}
