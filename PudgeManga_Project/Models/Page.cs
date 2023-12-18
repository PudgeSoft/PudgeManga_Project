using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PudgeManga_Project.Models
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }
        [ForeignKey("Chapter")]
        public int ChapterId { get; set; }
        public int PageNumber { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<PageComment> PageComments { get; set; }
        public Chapter Chapter { get; set; }
    }
}
