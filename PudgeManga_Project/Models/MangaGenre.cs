using Microsoft.EntityFrameworkCore;

namespace PudgeManga_Project.Models
{
    [Keyless]
    public class MangaGenre
    {
        public int MangaId { get; set; }
        public Manga Manga { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
