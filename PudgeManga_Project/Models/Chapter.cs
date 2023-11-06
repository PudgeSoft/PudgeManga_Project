using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PudgeManga_Project.Models
{
    public class Chapter
    {
        [Key]
        public int ChapterId { get; set; }
        public int MangaID { get; set; }
        public int ChapterNumber { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Url { get; set; }

        public ICollection<Page> Pages { get; set; }
        public Manga Manga { get; set; }
    }
}
