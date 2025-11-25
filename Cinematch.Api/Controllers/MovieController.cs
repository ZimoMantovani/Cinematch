using Cinematch.Api.Data;
using Cinematch.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cinematch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            // Tenta pegar o ID do token
            var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Se achou, retorna ele. Se não achou (null), retorna um fixo para teste.
            return !string.IsNullOrEmpty(idString) ? idString : "usuario_teste_sqlite";
        }

        [HttpPost("watch")]
        public async Task<IActionResult> WatchMovie([FromBody] WatchedMovie movie)
        {
            movie.UserId = GetUserId();

            _context.WatchedMovies.Add(movie);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("rate")]
        public async Task<IActionResult> RateMovie([FromBody] UserRating rating)
        {
            rating.UserId = GetUserId();

            _context.UserRatings.Add(rating);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("myprofile")]
        public async Task<ActionResult<UserProfile>> GetMyProfile()
        {
            var userId = GetUserId();

            // Busca no banco filtrando por esse ID
            var watched = await _context.WatchedMovies
                                        .Where(w => w.UserId == userId)
                                        .ToListAsync();

            var ratings = await _context.UserRatings
                                        .Where(r => r.UserId == userId)
                                        .ToListAsync();

            return Ok(new UserProfile
            {
                WatchedMovies = watched,
                UserRatings = ratings
            });
        }
    }

    public class UserProfile
    {
        // "= new();" evita erros de lista nula
        public List<WatchedMovie> WatchedMovies { get; set; } = new();
        public List<UserRating> UserRatings { get; set; } = new();
    }
}