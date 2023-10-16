
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Models;

namespace PudgeManga_Project.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Popularity> Popularities { get; set; }
        public DbSet<User> Users { get; set; }
    }
}