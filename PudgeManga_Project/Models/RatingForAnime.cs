using System.ComponentModel.DataAnnotations.Schema;

namespace PudgeManga_Project.Models
{
    public class RatingForAnime
    {
        public int Id { get; set; }
        [ForeignKey("Anime")]
        public int AnimeId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public double Value { get; set; }

        public Anime Anime { get; set; }
        public User User { get; set; }
    }
}
