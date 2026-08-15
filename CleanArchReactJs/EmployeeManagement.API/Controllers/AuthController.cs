using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Auth;
using EmployeeManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthService authService,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        // ------------------------------------------------------------------
        // Register
        // ------------------------------------------------------------------

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterDto dto,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            await _authService.RegisterAsync(dto);

            return Ok(ApiResponse<string>.Ok(
                "Registration completed successfully.",
                "User registered successfully."));
        }

        // ------------------------------------------------------------------
        // Login
        // ------------------------------------------------------------------

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ValidationProblem(ModelState);

                var result =
                    await _authService.LoginAsync(
                        dto
                        );

                if (!result.Success)
                    return Unauthorized(result);

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        // ------------------------------------------------------------------
        // Refresh Token
        // ------------------------------------------------------------------

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenDto dto,
            CancellationToken cancellationToken)
        {
            var result =
                await _authService.RefreshTokenAsync(
                    dto
                    );

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        // ------------------------------------------------------------------
        // Logout
        // ------------------------------------------------------------------

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(
            CancellationToken cancellationToken)
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userId, out var id))
                return Unauthorized();

            await _authService.LogoutAsync(
                id
                );

            return Ok(ApiResponse<string>.Ok(
                string.Empty,
                "Logout successful."));
        }

        // ------------------------------------------------------------------
        // Current User
        // ------------------------------------------------------------------

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var user = new
            {
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier),

                UserName = User.Identity?.Name,

                Email = User.FindFirstValue(ClaimTypes.Email),

                Role = User.FindFirstValue(ClaimTypes.Role)
            };

            return Ok(user);
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            return Ok(new
            {
                User.Identity?.Name,

                Claims = User.Claims.Select(c => new
                {
                    c.Type,
                    c.Value
                })
            });
        }

    }

}
