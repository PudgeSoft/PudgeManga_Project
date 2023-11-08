﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PudgeManga_Project.Models
{
    public class User: IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
