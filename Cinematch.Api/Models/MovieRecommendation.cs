namespace Cinematch.Api.Models
{
    // Objeto de Transferência de Dados (DTO).
    // Usamos esta classe para formatar a resposta JSON limpa que será enviada ao Frontend
    // após o quiz ser finalizado.
    public class MovieRecommendation
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; } // Sinopse do filme
        public string PosterUrl { get; set; }
    }
}