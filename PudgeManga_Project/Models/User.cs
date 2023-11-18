using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PudgeManga_Project.Models
{
    public class User: IdentityUser
    {
        [Key]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public string State { get; set; }

 
    }
}
