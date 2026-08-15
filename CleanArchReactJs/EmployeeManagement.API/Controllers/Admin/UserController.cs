using EmployeeManagement.Application.DTOs.Admin.Users;
using EmployeeManagement.Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers.Admin
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        //[Authorize(Users = "Admin,HR")]
        //[Authorize(Policy = Users.UserCreate)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _userService.CreateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        //[Authorize(Users = "Admin,HR")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Route id does not match request body.");

            var result = await _userService.UpdateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        //[Authorize(Users = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        [HttpDelete("{id:guid}/deletepermanent")]
        public async Task<IActionResult> DeletePermanent(Guid id)
        {
            var result = await _userService.DeletePermanentAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // [Authorize(Users = "Admin")]
        [HttpPost("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id)
        {
            var result = await _userService.RestoreAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchUserDto dto)
        {
            var result = await _userService.SearchAsync(dto);
            return Ok(result);
        }
    }
}
