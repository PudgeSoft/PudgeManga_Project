
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Models;

namespace PudgeManga_Project.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            //Database.EnsureCreated();
            //Database.Migrate();
        }

        public DbSet<Manga> Mangas => Set<Manga>();
        public DbSet<Chapter> Chapters => Set<Chapter>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Page> Pages => Set<Page>();
        public DbSet<Popularity> Popularities => Set<Popularity>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Genre> Genres => Set<Genre>();

        public DbSet<Anime> Animes => Set<Anime>();
        public DbSet<AnimeEpisode> AnimesEpisodes => Set<AnimeEpisode>();
        public DbSet<AnimeSeason> AnimeSeasons => Set<AnimeSeason>();
        public DbSet<GenreForAnime> GenresForAnimes => Set<GenreForAnime>();
        public DbSet<Rating> Ratings => Set<Rating>();

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

            modelBuilder.Entity<AnimeGenre>()
                .HasKey(ag => new { ag.AnimeId, ag.GenreId });

            modelBuilder.Entity<AnimeGenre>()
                .HasOne(ag => ag.Anime)
                .WithMany(a => a.AnimeGenres)
                .HasForeignKey(ag => ag.AnimeId);

            modelBuilder.Entity<AnimeGenre>()
                .HasOne(ag => ag.GenreForAnime)
                .WithMany(g => g.AnimeGenres)
                .HasForeignKey(ag => ag.GenreId);
            base.OnModelCreating(modelBuilder);
        }


    }
}