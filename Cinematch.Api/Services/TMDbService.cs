using Cinematch.Api.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Cinematch.Api.Services
{
    public class TMDbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly Dictionary<string, int> _genreMap = new Dictionary<string, int>
        {
            { "Ação", 28 },
            { "Aventura", 12 },
            { "Comédia", 35 },
            { "Drama", 18 },
            { "Fantasia", 14 },
            { "Terror", 27 },
            { "Ficção Científica", 878 },
            { "Romance", 10749 }
            // Adicione outros gêneros conforme necessário
        };

        public TMDbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["TMDbSettings:ApiKey"];
            _baseUrl = configuration["TMDbSettings:BaseUrl"];
        }

        public async Task<MovieRecommendation> GetRandomPopularMovieByGenre(string genreName)
        {
            if (!_genreMap.TryGetValue(genreName, out int genreId))
            {
                return null; // Gênero não mapeado
            }

            var url = $"{_baseUrl}discover/movie?api_key={_apiKey}&with_genres={genreId}&language=pt-BR&sort_by=popularity.desc&page=1";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null; // Erro na API do TMDb
            }

            var content = await response.Content.ReadAsStringAsync();
            using (JsonDocument document = JsonDocument.Parse(content))
            {
                var results = document.RootElement.GetProperty("results").EnumerateArray().ToList();

                if (!results.Any())
                {
                    return null; // Nenhum filme encontrado
                }

                // Sorteia um filme aleatoriamente dos 20 primeiros
                var random = new System.Random();
                var selectedMovie = results[random.Next(results.Count)];

                var posterPath = selectedMovie.GetProperty("poster_path").GetString();
                var posterUrl = string.IsNullOrEmpty(posterPath) ? "" : $"https://image.tmdb.org/t/p/w500{posterPath}";

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
