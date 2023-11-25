using System.ComponentModel.DataAnnotations;

namespace PudgeManga_Project.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string Title {get; set; }

        public ICollection<MangaGenre> MangaGenres { get; set; }

    }
}
