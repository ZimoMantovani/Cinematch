using Cinematch.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinematch.Api.Data
{
    // Esta classe herda de 'DbContext', que é o coração do Entity Framework.
    // Ela atua como uma sessão com o banco de dados, permitindo consultar e salvar dados.
    public class AppDbContext : DbContext
    {
        // CONSTRUTOR:
        // Recebe as opções de configuração (como a string de conexão "Data Source=Cinematch.db")
        // que foram definidas no Program.cs e repassa para a classe base.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // REPRESENTAÇÃO DAS TABELAS:
        // Cada 'DbSet' abaixo vira uma Tabela no SQLite.
        // O nome da propriedade (ex: Users) será o nome da tabela no banco.

        public DbSet<User> Users { get; set; }              // Tabela de Usuários
        public DbSet<UserRating> UserRatings { get; set; }  // Tabela de Avaliações
        public DbSet<WatchedMovie> WatchedMovies { get; set; } // Tabela de Filmes Vistos

        // CONFIGURAÇÕES AVANÇADAS (Fluent API):
        // Este método roda no momento em que o modelo está sendo criado.
        // Usamos ele para configurações manuais que não puderam ser feitas com Data Annotations.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Garante o comportamento padrão do EF Core
            base.OnModelCreating(modelBuilder);
        }
    }
}