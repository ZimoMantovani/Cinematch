using Cinematch.Api.Models;
using Cinematch.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinematch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly QuizLogicService _quizLogicService;
        private readonly TMDbService _tmdbService;

        public QuizController(QuizLogicService quizLogicService, TMDbService tmdbService)
        {
            _quizLogicService = quizLogicService;
            _tmdbService = tmdbService;
        }

        [HttpGet("questions")]
        public ActionResult<List<QuizQuestion>> GetQuestions()
        {
            return Ok(_quizLogicService.GetQuestions());
        }

        [HttpPost("recommend")]
        public async Task<ActionResult<MovieRecommendation>> RecommendMovie([FromBody] string[] answers)
        {
            if (answers == null || answers.Length != 6)
            {
                return BadRequest("É necessário fornecer 6 respostas para o quiz.");
            }

            // 1. Processa as respostas para determinar o gênero
            var quizResult = _quizLogicService.ProcessAnswers(answers);

            // 2. Busca um filme com base no gênero
            var recommendation = await _tmdbService.GetRandomPopularMovieByGenre(quizResult.Genre);

            if (recommendation == null)
            {
                // Tratamento de Erro (RF 1.3)
                return NotFound(new { message = "Ops, não encontramos um filme. Tente o quiz novamente." });
            }

            return Ok(recommendation);
        }
    }
}
