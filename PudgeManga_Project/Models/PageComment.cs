using Microsoft.EntityFrameworkCore;

namespace PudgeManga_Project.Models
{
    [Keyless]
    public class PageComment
    {
        public int PageId { get; set; }
        public Page Page { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }

}
