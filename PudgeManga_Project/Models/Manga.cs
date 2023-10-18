using System.ComponentModel.DataAnnotations;

namespace PudgeManga_Project.Models
{
    public class Manga
    {
        [Key]
        public int MangaId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public int GenreId { get; set; }

        public ICollection<Chapter> Chapters { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Popularity Popularity { get; set; }
    }
}
