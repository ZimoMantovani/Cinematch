using Cinematch.Api.Models;
using Cinematch.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinematch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.Authenticate(request.Email, request.Password);

            if (token == null)
            {
                return Unauthorized(new { message = "E-mail ou senha inválidos." });
            }

            return Ok(new { token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            var user = await _authService.Register(request.Email, request.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Usuário já existe." });
            }

            var token = await _authService.Authenticate(request.Email, request.Password);
            return Ok(new { token = token });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
