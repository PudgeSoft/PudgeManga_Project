using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PudgeManga_Project.Models
{
    public class Popularity
    {
        [Key]
        [ForeignKey("Manga")]
        public int MangaId { get; set; }
        public int ViewsCount { get; set; }
        public int CommentsCount { get; set; }
        public decimal AverageRating { get; set; }

        public Manga Manga { get; set; }
    }
}
