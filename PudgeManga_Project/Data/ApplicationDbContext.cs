
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Models;

namespace PudgeManga_Project.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Manga> Mangas => Set<Manga>();
        public DbSet<Chapter> Chapters => Set<Chapter>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Page> Pages => Set<Page>();
        public DbSet<Popularity> Popularities => Set<Popularity>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Genre> Genres => Set<Genre>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MangaGenre>()
                .HasKey(mg => new { mg.MangaId, mg.GenreId });

            modelBuilder.Entity<MangaGenre>()
                .HasOne(mg => mg.Manga)
                .WithMany(m => m.MangaGenres) 
                .HasForeignKey(mg => mg.MangaId);

            modelBuilder.Entity<MangaGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MangaGenres)
                .HasForeignKey(mg => mg.GenreId);

            base.OnModelCreating(modelBuilder);
        }


    }
}