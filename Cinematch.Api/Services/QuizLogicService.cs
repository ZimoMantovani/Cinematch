using Cinematch.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cinematch.Api.Services
{
    // Esta classe encapsula a "Regra de Negócio" do Quiz.
    // O Controller não precisa saber COMO a pontuação é calculada, apenas pede o resultado.
    public class QuizLogicService
    {
        // ESTRUTURA DE PONTUAÇÃO (Weighted Scoring System)
        // Mapeamos cada resposta possível para um peso em determinados gêneros.
        // Exemplo: Escolher "Ação" na pergunta 1 dá +2 pontos para o gênero "Ação".
        private readonly Dictionary<string, Dictionary<string, int>> _scoreMap = new Dictionary<string, Dictionary<string, int>>
        {
            // Pergunta 1: Como você está se sentindo agora?
            { "Q1A", new Dictionary<string, int> { { "Ação", 2 }, { "Aventura", 1 } } },
            { "Q1B", new Dictionary<string, int> { { "Comédia", 3 } } },
            { "Q1C", new Dictionary<string, int> { { "Drama", 2 }, { "Romance", 1 } } },
            { "Q1D", new Dictionary<string, int> { { "Ficção Científica", 1 }, { "Fantasia", 2 } } }, 

            // ... (Outras perguntas mapeadas da mesma forma) ...
            // Omitido aqui para brevidade, mas a lógica segue o padrão: Resposta -> Gêneros -> Pontos
             { "Q2A", new Dictionary<string, int> { { "Romance", 3 }, { "Comédia", 1 } } }, // Com meu par romântico
            { "Q2B", new Dictionary<string, int> { { "Ação", 2 }, { "Aventura", 2 } } }, // Com um grupo de amigos
            { "Q2C", new Dictionary<string, int> { { "Terror", 3 }, { "Drama", 1 } } }, // Sozinho, no escuro
            { "Q2D", new Dictionary<string, int> { { "Ficção Científica", 2 }, { "Fantasia", 2 } } }, // Com a família
            
            // ... (Repete para Q3, Q4, Q5, Q6)
             // Pergunta 3: Qual o seu tipo de cenário favorito?
            { "Q3A", new Dictionary<string, int> { { "Ação", 2 }, { "Ficção Científica", 2 } } }, // Cidades futuristas ou espaço
            { "Q3B", new Dictionary<string, int> { { "Aventura", 3 }, { "Fantasia", 1 } } }, // Florestas densas ou ruínas antigas
            { "Q3C", new Dictionary<string, int> { { "Drama", 2 }, { "Romance", 2 } } }, // Uma cidade pequena e aconchegante
            { "Q3D", new Dictionary<string, int> { { "Terror", 3 } } }, // Uma casa abandonada ou escura

            // Pergunta 4: Qual emoção você busca sentir?
            { "Q4A", new Dictionary<string, int> { { "Comédia", 3 }, { "Romance", 1 } } }, // Alegria e leveza
            { "Q4B", new Dictionary<string, int> { { "Ação", 3 }, { "Aventura", 1 } } }, // Adrenalina e empolgação
            { "Q4C", new Dictionary<string, int> { { "Drama", 3 } } }, // Profundidade e reflexão
            { "Q4D", new Dictionary<string, int> { { "Terror", 3 }, { "Ficção Científica", 1 } } }, // Medo e suspense

            // Pergunta 5: Qual elemento narrativo te atrai mais?
            { "Q5A", new Dictionary<string, int> { { "Ficção Científica", 3 }, { "Fantasia", 2 } } }, // Magia, superpoderes ou tecnologia avançada
            { "Q5B", new Dictionary<string, int> { { "Ação", 2 }, { "Aventura", 3 } } }, // Uma jornada épica ou busca por um tesouro
            { "Q5C", new Dictionary<string, int> { { "Comédia", 2 }, { "Romance", 2 } } }, // Diálogos inteligentes e relacionamentos complexos
            { "Q5D", new Dictionary<string, int> { { "Drama", 2 }, { "Terror", 1 } } }, // Um mistério a ser resolvido ou um dilema moral

            // Pergunta 6: Qual o ritmo ideal para o filme?
            { "Q6A", new Dictionary<string, int> { { "Ação", 3 }, { "Aventura", 2 } } }, // Rápido, com muitas cenas de luta/perseguição
            { "Q6B", new Dictionary<string, int> { { "Comédia", 2 }, { "Romance", 2 } } }, // Moderado, com momentos de riso e emoção
            { "Q6C", new Dictionary<string, int> { { "Drama", 3 } } }, // Lento, focado no desenvolvimento dos personagens
            { "Q6D", new Dictionary<string, int> { { "Terror", 2 }, { "Ficção Científica", 2 } } } // Imprevisível, com reviravoltas e sustos
        };

        // ALGORITMO DE PROCESSAMENTO
        // Recebe o array de respostas do usuário (ex: ["Q1A", "Q2C"...])
        public QuizResult ProcessAnswers(string[] answers)
        {
            var genreScores = new Dictionary<string, int>();

            // 1. Itera sobre cada resposta dada pelo usuário
            foreach (var answer in answers)
            {
                // 2. Verifica se a resposta existe no nosso mapa de pontuação
                if (_scoreMap.TryGetValue(answer, out var scores))
                {
                    // 3. Soma os pontos para cada gênero associado àquela resposta
                    foreach (var score in scores)
                    {
                        if (genreScores.ContainsKey(score.Key))
                        {
                            genreScores[score.Key] += score.Value;
                        }
                        else
                        {
                            genreScores.Add(score.Key, score.Value);
                        }
                    }
                }
            }

            // 4. Ordena os gêneros do maior para o menor e pega o vencedor
            var winner = genreScores.OrderByDescending(kv => kv.Value).FirstOrDefault();

            return new QuizResult
            {
                Genre = winner.Key ?? "Ação", // Fallback (Padrão) caso algo dê errado
                Score = winner.Value
            };
        }

        // Método simples que retorna os dados estáticos das perguntas para o Frontend
        public List<QuizQuestion> GetQuestions()
        {
            return new List<QuizQuestion>
            {
                // ... (Definição das perguntas - serve como fonte de dados estática) ...
                new QuizQuestion
                {
                    Id = 1,
                    Text = "Como você está se sentindo agora?",
                    Options = new Dictionary<string, string>
                    {
                        { "A", "Animado e cheio de energia" },
                        { "B", "Querendo rir um pouco" },
                        { "C", "Reflexivo e pensativo" },
                        { "D", "Curioso e imaginativo" }
                    }
                },
                // ... (Outras perguntas)
                new QuizQuestion
                {
                    Id = 2,
                    Text = "Com quem você assistiria ao filme?",
                    Options = new Dictionary<string, string>
                    {
                        { "A", "Com meu par romântico" },
                        { "B", "Com um grupo de amigos" },
                        { "C", "Sozinho, no escuro" },
                        { "D", "Com a família" }
                    }
                },
                new QuizQuestion
                {
                    Id = 3,
                    Text = "Qual o seu tipo de cenário favorito?",
                    Options = new Dictionary<string, string>
                    {
                        { "A", "Cidades futuristas ou espaço" },
                        { "B", "Florestas densas ou ruínas antigas" },
                        { "C", "Uma cidade pequena e aconchegante" },
                        { "D", "Uma casa abandonada ou escura" }
                    }
                },
                new QuizQuestion
                {
                    Id = 4,
                    Text = "Qual emoção você busca sentir?",
                    Options = new Dictionary<string, string>
                    {
                        { "A", "Alegria e leveza" },
                        { "B", "Adrenalina e empolgação" },
                        { "C", "Profundidade e reflexão" },
                        { "D", "Medo e suspense" }
                    }
                },
                new QuizQuestion
                {
                    Id = 5,
                    Text = "Qual elemento narrativo te atrai mais?",
                    Options = new Dictionary<string, string>
                    {
                        { "A", "Magia, superpoderes ou tecnologia avançada" },
                        { "B", "Uma jornada épica ou busca por um tesouro" },
                        { "C", "Diálogos inteligentes e relacionamentos complexos" },
                        { "D", "Um mistério a ser resolvido ou um dilema moral" }
                    }
                },
                new QuizQuestion
                {
                    Id = 6,
                    Text = "Qual o ritmo ideal para o filme?",
                    Options = new Dictionary<string, string>
                    {
                        { "A", "Rápido, com muitas cenas de luta/perseguição" },
                        { "B", "Moderado, com momentos de riso e emoção" },
                        { "C", "Lento, focado no desenvolvimento dos personagens" },
                        { "D", "Imprevisível, com reviravoltas e sustos" }
                    }
                }
            };
        }
    }

    public class QuizQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Dictionary<string, string> Options { get; set; }
    }
}