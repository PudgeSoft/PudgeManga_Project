using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace PudgeManga_Project.Models
{
    public class GenreForAnime
    {
        [Key]
        public int GenreId { get; set; }
        public string Title {  get; set; }

        public ICollection<AnimeGenre> AnimeGenres { get; set; }
    }
}
