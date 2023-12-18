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
        public string Type { get; set; }
        public string Publish { get; set; }
        public string Artist { get; set; }
        public string Translator { get; set; }


        public ICollection<MangaGenre> MangaGenres { get; set; }
        public ICollection<Chapter> Chapters { get; set; }
        public ICollection<MangaComment> MangaComments { get; set; }
        public ICollection<RatingForManga> Ratings { get; set; }
        public Popularity Popularity { get; set; }
    }

}
