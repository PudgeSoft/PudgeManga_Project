using Microsoft.EntityFrameworkCore;

namespace PudgeManga_Project.Models
{
    [Keyless]
    public class UserComment
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }

}
