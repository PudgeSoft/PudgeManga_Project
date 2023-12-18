using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace PudgeManga_Project.Models
{
    public class User : IdentityUser
    {
        [Key]
        public string Description { get; set; }
        public string? Image { get; set; }
        public int Age { get; set; }
        public string? State { get; set; }
        public string? Aboutme { get; set; }
        public ICollection<UserComment> UserComments { get; set; } = new List<UserComment>();
        public ICollection<RatingForManga> Ratings { get; set; }

    }
}
