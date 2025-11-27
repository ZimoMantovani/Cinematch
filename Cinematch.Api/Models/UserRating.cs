using System;

namespace Cinematch.Api.Models
{
    // Representa a tabela 'UserRatings'.
    public class UserRating
    {
        // Identificador único da avaliação.
        public int Id { get; set; }

        // Quem avaliou?
        public string? UserId { get; set; }

        // Qual filme? (ID do TMDb)
        public int MovieId { get; set; }

        // A nota dada pelo usuário (escala de 1 a 5).
        public int Rating { get; set; }

        // O comentário escrito (Crítica). O '?' indica que é opcional (pode ser nulo).
        public string? ReviewText { get; set; }

        // Dados cacheados do filme para exibição rápida no perfil.
        public string? MovieTitle { get; set; }
        public string? MoviePosterUrl { get; set; }

        // Data da avaliação.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}