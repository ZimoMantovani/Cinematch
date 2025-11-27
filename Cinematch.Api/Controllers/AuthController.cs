using Cinematch.Api.Models;
using Cinematch.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinematch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Serviço responsável pela lógica de criptografia e geração de Token
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/login
        // Recebe email/senha e devolve um Token JWT se estiver correto.
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Chama o serviço para verificar se o usuário existe no banco
            var token = await _authService.Authenticate(request.Email, request.Password);

            // Se o token vier nulo, significa que a senha estava errada
            if (token == null)
            {
                return Unauthorized(new { message = "E-mail ou senha inválidos." });
            }

            // Retorna o Token para o Frontend salvar (geralmente no LocalStorage)
            return Ok(new { token = token });
        }

        // POST: api/auth/register
        // Cria um novo usuário no banco de dados.
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            // Tenta registrar
            var user = await _authService.Register(request.Email, request.Password);

            // Se devolver nulo, é porque o email já existe
            if (user == null)
            {
                return BadRequest(new { message = "Usuário já existe." });
            }

            // UX: Após registrar, já faz o login automático gerando o token
            var token = await _authService.Authenticate(request.Email, request.Password);
            return Ok(new { token = token });
        }
    }

    // DTO (Data Transfer Object) para receber apenas o necessário do JSON
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}