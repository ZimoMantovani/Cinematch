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
        // Serviço que contém a lógica das perguntas e cálculo de pontuação
        private readonly QuizLogicService _quizLogicService;

        // Serviço que conecta com a API Externa do The Movie Database (TMDb)
        private readonly TMDbService _tmdbService;

        public QuizController(QuizLogicService quizLogicService, TMDbService tmdbService)
        {
            _quizLogicService = quizLogicService;
            _tmdbService = tmdbService;
        }

        // GET: api/quiz/questions
        // Retorna a lista de perguntas para o Frontend montar o Quiz
        [HttpGet("questions")]
        public ActionResult<List<QuizQuestion>> GetQuestions()
        {
            return Ok(_quizLogicService.GetQuestions());
        }

        // POST: api/quiz/recommend
        // Recebe as respostas do usuário e devolve um filme recomendado
        [HttpPost("recommend")]
        public async Task<ActionResult<MovieRecommendation>> RecommendMovie([FromBody] string[] answers)
        {
            // Validação simples: O quiz tem que ter 6 respostas
            if (answers == null || answers.Length != 6)
            {
                return BadRequest("É necessário fornecer 6 respostas para o quiz.");
            }

            // PASSO 1: Lógica interna
            // Processa as respostas (A, B, C...) para descobrir qual gênero o usuário quer (ex: Ação, Terror)
            var quizResult = _quizLogicService.ProcessAnswers(answers);

            // PASSO 2: API Externa
            // Usa o gênero descoberto para buscar um filme aleatório na API do TMDb
            var recommendation = await _tmdbService.GetRandomPopularMovieByGenre(quizResult.Genre);

            // Tratamento de Erro (Requisito funcional): Se a API falhar
            if (recommendation == null)
            {
                return NotFound(new { message = "Ops, não encontramos um filme. Tente o quiz novamente." });
            }

            // Devolve o filme pronto para exibir na tela de Resultado
            return Ok(recommendation);
        }
    }
}