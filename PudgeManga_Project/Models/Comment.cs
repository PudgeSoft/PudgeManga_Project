using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PudgeManga_Project.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Manga")]
        public int MangaId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        public User User { get; set; }
        public Manga Manga { get; set; }
    }
}
