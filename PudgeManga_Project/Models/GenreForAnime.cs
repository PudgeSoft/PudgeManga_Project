using System.ComponentModel.DataAnnotations;

namespace PudgeManga_Project.Models
{
    public class GenreForAnime
    {
        [Key]
        public int AnimeGenreId { get; set; }
        public string Name {  get; set; }

        public ICollection<AnimeGenre> AnimeGenres { get; set; }
    }
}
