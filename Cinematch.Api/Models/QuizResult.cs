namespace Cinematch.Api.Models
{
    // Esta classe NÃO é uma tabela do banco.
    // Ela é um objeto auxiliar (DTO) usado apenas na memória durante o processamento do Quiz.
    public class QuizResult
    {
        // O gênero vencedor calculado pelo algoritmo (ex: "Action", "Horror").
        public string Genre { get; set; }

        // A pontuação atingida (usada internamente pela lógica de recomendação).
        public int Score { get; set; }
    }
}