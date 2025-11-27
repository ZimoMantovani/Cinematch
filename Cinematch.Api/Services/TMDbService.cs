using Cinematch.Api.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Cinematch.Api.Services
{
    // Serviço responsável pela comunicação com a API Externa (The Movie Database).
    // Isola toda a complexidade de HTTP e JSON do resto do sistema.
    public class TMDbService
    {
        private readonly HttpClient _httpClient; // Usado para fazer requisições GET
        private readonly string _apiKey;
        private readonly string _baseUrl;

        // Dicionário para traduzir nossos gêneros (string) para os IDs numéricos que o TMDb exige.
        private readonly Dictionary<string, int> _genreMap = new Dictionary<string, int>
        {
            { "Ação", 28 },
            { "Aventura", 12 },
            { "Comédia", 35 },
            { "Drama", 18 },
            // ... etc
             { "Fantasia", 14 },
            { "Terror", 27 },
            { "Ficção Científica", 878 },
            { "Romance", 10749 }
        };

        public TMDbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // Lê as chaves sensíveis do arquivo appsettings.json (segurança)
            _apiKey = configuration["TMDbSettings:ApiKey"];
            _baseUrl = configuration["TMDbSettings:BaseUrl"];
        }

        public async Task<MovieRecommendation> GetRandomPopularMovieByGenre(string genreName)
        {
            // 1. Traduz o nome do gênero (ex: "Ação") para o ID (ex: 28)
            if (!_genreMap.TryGetValue(genreName, out int genreId))
            {
                return null; // Gênero não mapeado
            }

            // 2. Monta a URL da API com Query Parameters (Filtra por gênero, ordena por popularidade)
            var url = $"{_baseUrl}discover/movie?api_key={_apiKey}&with_genres={genreId}&language=pt-BR&sort_by=popularity.desc&page=1";

            // 3. Faz a requisição assíncrona
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null; // Falha na comunicação com a API externa
            }

            // 4. Lê e processa (Parse) o JSON retornado
            var content = await response.Content.ReadAsStringAsync();
            using (JsonDocument document = JsonDocument.Parse(content))
            {
                var results = document.RootElement.GetProperty("results").EnumerateArray().ToList();

                if (!results.Any()) return null;

                // 5. Lógica de Aleatoriedade: Pega um filme qualquer da primeira página de resultados
                var random = new System.Random();
                var selectedMovie = results[random.Next(results.Count)];

                // Tratamento da URL da imagem
                var posterPath = selectedMovie.GetProperty("poster_path").GetString();
                var posterUrl = string.IsNullOrEmpty(posterPath) ? "" : $"https://image.tmdb.org/t/p/w500{posterPath}";

                // 6. Retorna nosso objeto simplificado (DTO) apenas com o que o Frontend precisa
                return new MovieRecommendation
                {
                    MovieId = selectedMovie.GetProperty("id").GetInt32(),
                    Title = selectedMovie.GetProperty("title").GetString(),
                    Overview = selectedMovie.GetProperty("overview").GetString(),
                    PosterUrl = posterUrl
                };
            }
        }
    }
}