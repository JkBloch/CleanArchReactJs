using EmployeeManagement.Application.DTOs.Roles;
using EmployeeManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _roleService.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        //[Authorize(Roles = "Admin,HR")]
        //[Authorize(Policy = Roles.RoleCreate)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _roleService.CreateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        //[Authorize(Roles = "Admin,HR")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Route id does not match request body.");

            var result = await _roleService.UpdateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _roleService.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        [HttpDelete("{id:guid}/deletepermanent")]
        public async Task<IActionResult> DeletePermanent(Guid id)
        {
            var result = await _roleService.DeletePermanentAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // [Authorize(Roles = "Admin")]
        [HttpPost("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id)
        {
            var result = await _roleService.RestoreAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchRoleDto dto)
        {
            var result = await _roleService.SearchAsync(dto);
            return Ok(result);
        }
    }
}
