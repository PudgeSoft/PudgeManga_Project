
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Models;

namespace PudgeManga_Project.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() => Database.EnsureCreated();

        public DbSet<Manga> Mangas => Set<Manga>();
        public DbSet<Chapter> Chapters => Set<Chapter>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Page> Pages => Set<Page>();
        public DbSet<Popularity> Popularities => Set<Popularity>();
        public DbSet<User> Users => Set<User>();
    }
}