using Cinematch.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinematch.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<WatchedMovie> WatchedMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais de modelo, se necessário
            base.OnModelCreating(modelBuilder);
        }
    }
}
