using System;

namespace Cinematch.Api.Models
{
    public class UserRating
    {
        public int Id { get; set; }
        public string? UserId { get; set; } 
        public int MovieId { get; set; }
        public int Rating { get; set; } // 1-5
        public string? ReviewText { get; set; }
        public string? MovieTitle { get; set; }
        public string? MoviePosterUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
