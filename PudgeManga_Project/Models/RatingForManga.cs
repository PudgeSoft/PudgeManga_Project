using System.ComponentModel.DataAnnotations.Schema;

namespace PudgeManga_Project.Models
{
    public class RatingForManga
    {
        public int Id { get; set; }
        [ForeignKey("Manga")]
        public int MangaId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public double Value { get; set; }

        public Manga Manga { get; set; }
        public User User { get; set; }
    }
}
