using Microsoft.EntityFrameworkCore;

namespace PudgeManga_Project.Models
{
    [Keyless]
    public class AnimeSeasonComment
    {
        public int AnimeSeasonId { get; set; }
        public AnimeSeason AnimeSeason { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
