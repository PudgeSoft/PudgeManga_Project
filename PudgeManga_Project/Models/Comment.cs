using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PudgeManga_Project.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }

        public int? ParentId { get; set; }
        public Comment ParentComment { get; set; }

        public ICollection<UserComment> UserComments { get; set; } 
        public ICollection<MangaComment> MangaComments { get; set; } 
        public ICollection<AnimeSeasonComment> AnimeSeasonComments { get; set; }
        public ICollection<PageComment> PageComments { get; set; } 
    }

}
