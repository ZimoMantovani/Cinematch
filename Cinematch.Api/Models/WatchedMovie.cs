using System;

namespace Cinematch.Api.Models
{
    public class WatchedMovie
    {
        public int Id { get; set; }
        public string? UserId { get; set; } 
        public int MovieId { get; set; }
        public string? MovieTitle { get; set; }
        public string? MoviePosterUrl { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
