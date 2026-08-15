using EmployeeManagement.Application.DTOs.Admin.UserRoles;
using EmployeeManagement.Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers.Admin
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userRoleService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userRoleService.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        //[Authorize(UserRoles = "Admin,HR")]
        //[Authorize(Policy = UserRoles.UserRoleCreate)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRoleDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _userRoleService.CreateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        //[Authorize(UserRoles = "Admin,HR")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRoleDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Route id does not match request body.");

            var result = await _userRoleService.UpdateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        //[Authorize(UserRoles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userRoleService.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        [HttpDelete("{id:guid}/deletepermanent")]
        public async Task<IActionResult> DeletePermanent(Guid id)
        {
            var result = await _userRoleService.DeletePermanentAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // [Authorize(UserRoles = "Admin")]
        [HttpPost("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id)
        {
            var result = await _userRoleService.RestoreAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchUserRoleDto dto)
        {
            var result = await _userRoleService.SearchAsync(dto);
            return Ok(result);
        }
    }
}
