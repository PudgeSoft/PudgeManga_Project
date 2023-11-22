﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace PudgeManga_Project.Models
{
    public class User: IdentityUser
    {
        [Key]
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? State { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
