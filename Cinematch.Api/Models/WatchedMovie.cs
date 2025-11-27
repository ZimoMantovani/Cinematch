using System;

namespace Cinematch.Api.Models
{
    // Representa a tabela 'WatchedMovies'.
    // Relaciona um usuário a um filme que ele marcou como visto.
    public class WatchedMovie
    {
        // Chave Primária do registro no banco local.
        public int Id { get; set; }

        // Chave Estrangeira (FK): Quem assistiu?
        // (Armazenamos o ID do usuário como string ou int dependendo da lógica de Auth).
        public string? UserId { get; set; }

        // ID do filme na API Externa (TMDb). 
        // Não salvamos todos os dados do filme aqui, só o ID para buscar lá depois se precisar.
        public int MovieId { get; set; }

        // Cache: Salvamos Título e Poster aqui para não precisar consultar a API do TMDb 
        // toda vez que formos listar o perfil (melhora a performance).
        public string? MovieTitle { get; set; }
        public string? MoviePosterUrl { get; set; }

        // Data de quando foi marcado (Preenchido automaticamente com a data de agora).
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}