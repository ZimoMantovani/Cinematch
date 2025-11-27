using Cinematch.Api.Data;
using Cinematch.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Cinematch.Api.Services
{
    // Serviço responsável pela Autenticação e Autorização.
    // Lida com o Banco de Dados de usuários e com a Criptografia do Token.
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration; // Para ler a "Chave Secreta" do JWT

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Verifica credenciais (Login)
        public async Task<string?> Authenticate(string email, string password)
        {
            // Busca o usuário no banco SQLite
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            // Validação simples: compara a senha enviada com a do banco.
            // OBS: Em produção real, usaríamos Hashing (BCrypt) aqui para não salvar senha pura.
            if (user == null || user.PasswordHash != password)
            {
                return null;
            }

            // Se deu certo, gera e entrega o Token JWT
            return GenerateJwtToken(user);
        }

        // Cria novo usuário (Registro)
        public async Task<User?> Register(string email, string password)
        {
            // Verifica duplicidade de email
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                return null;
            }

            // Salva no banco
            var user = new User { Email = email, PasswordHash = password };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // Lógica Core de Segurança: Geração do Token JWT
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            // Pega a chave secreta definida no appsettings.json
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // CLAIMS: São os dados que vão "dentro" do token.
                // Aqui estamos guardando o Email e o ID do usuário dentro do token criptografado.
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // O ID é vital para sabermos quem é quem depois
                }),
                // O token expira em 7 dias (o usuário precisa logar de novo depois disso)
                Expires = DateTime.UtcNow.AddDays(7),
                // Assina o token digitalmente usando HMAC SHA256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}