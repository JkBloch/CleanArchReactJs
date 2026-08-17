using EmployeeManagement.Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers.Admin
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ApplicationLogController : ControllerBase
    {
        private readonly IApplicationLogService _applicationLogService;
        public ApplicationLogController(IApplicationLogService applicationLogService)
        {
            _applicationLogService = applicationLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _applicationLogService.GetAllAsync();
            return Ok(result);
        }
        
         
        [HttpPost("search")]
        public async Task<IActionResult> Search([FromQuery] string? search)
        {
            var result = await _applicationLogService.SearchAsync(search);
            return Ok(result);
        }
    }
}
