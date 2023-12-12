using Microsoft.EntityFrameworkCore;

namespace PudgeManga_Project.Models
{
    [Keyless]
    public class MangaComment
    {
        public int MangaId { get; set; }
        public Manga Manga { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }

}
