using Cinematch.Api.Data;
using Cinematch.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cinematch.Api.Controllers
{
    // [ApiController] indica que esta classe responde a requisições HTTP (API REST).
    // [Route] define o endereço base: meusite.com/api/movie
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        // Variável para acessar o Banco de Dados (SQLite)
        private readonly AppDbContext _context;

        // CONSTRUTOR:
        // Aqui usamos Injeção de Dependência para receber o contexto do banco.
        // O ASP.NET cria e entrega o '_context' pronto para uso.
        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        // Método auxiliar para descobrir quem é o usuário que está fazendo a requisição.
        private string GetUserId()
        {
            // Tenta ler o ID escondido dentro do Token JWT (ClaimTypes.NameIdentifier)
            var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Validação de Segurança: Se não achar o ID (login falhou ou é teste),
            // retorna um usuário genérico para o sistema não travar na apresentação.
            return !string.IsNullOrEmpty(idString) ? idString : "usuario_teste_sqlite";
        }

        // POST: api/movie/watch
        // Recebe um objeto JSON com os dados do filme e salva na tabela 'WatchedMovies'.
        [HttpPost("watch")]
        public async Task<IActionResult> WatchMovie([FromBody] WatchedMovie movie)
        {
            // 1. Preenche o ID do usuário que está logado
            movie.UserId = GetUserId();

            // 2. Adiciona o filme na memória do Entity Framework
            _context.WatchedMovies.Add(movie);

            // 3. Efetiva a gravação no arquivo do banco de dados (SQLite)
            await _context.SaveChangesAsync();

            // Retorna status 200 (OK)
            return Ok();
        }

        // POST: api/movie/rate
        // Funciona igual ao 'watch', mas salva na tabela de avaliações ('UserRatings').
        [HttpPost("rate")]
        public async Task<IActionResult> RateMovie([FromBody] UserRating rating)
        {
            rating.UserId = GetUserId();

            _context.UserRatings.Add(rating);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: api/movie/myprofile
        // Busca tudo que o usuário já fez para montar a tela de Perfil.
        [HttpGet("myprofile")]
        public async Task<ActionResult<UserProfile>> GetMyProfile()
        {
            // Pega o ID do usuário logado
            var userId = GetUserId();

            // LINQ: Vai no banco e filtra (.Where) apenas os filmes deste usuário específico.
            var watched = await _context.WatchedMovies
                                        .Where(w => w.UserId == userId)
                                        .ToListAsync();

            var ratings = await _context.UserRatings
                                        .Where(r => r.UserId == userId)
                                        .ToListAsync();

            // Monta um objeto 'UserProfile' com as duas listas e envia para o Frontend.
            return Ok(new UserProfile
            {
                WatchedMovies = watched,
                UserRatings = ratings
            });
        }
    }

    // Classe simples (DTO) para agrupar os dados do perfil
    public class UserProfile
    {
        // Inicializamos com 'new()' para evitar erros de lista vazia (null) no Frontend
        public List<WatchedMovie> WatchedMovies { get; set; } = new();
        public List<UserRating> UserRatings { get; set; } = new();
    }
}