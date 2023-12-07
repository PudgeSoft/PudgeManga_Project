using System.ComponentModel.DataAnnotations.Schema;

namespace PudgeManga_Project.Models
{
    public class Rating
    {
        public int Id { get; set; }
        [ForeignKey("Manga")]
        public int MangaId { get; set; }
        [ForeignKey("Anime")]
        public int AnimeId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public int Value { get; set; }

        public Manga Manga { get; set; }
        public Anime Anime { get; set; }
        public User User { get; set; }
    }
}
