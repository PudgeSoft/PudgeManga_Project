
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
        public DbSet<RatingForManga> RatingForMangas => Set<RatingForManga>();
        public DbSet<RatingForAnime> RatingForAnimes => Set<RatingForAnime>();
        public DbSet<MangaComment> CommentsForManga => Set<MangaComment>();

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

            modelBuilder.Entity<AnimeSeasonComment>()
                .HasKey(x => new { x.AnimeSeasonId, x.CommentId });

            modelBuilder.Entity<AnimeSeasonComment>()
                .HasOne(ac => ac.AnimeSeason)
                .WithMany(an => an.AnimeSeasonComments)
                .HasForeignKey(ac => ac.AnimeSeasonId);

            modelBuilder.Entity<AnimeSeasonComment>()
                .HasOne(ac => ac.Comment)
                .WithMany(c => c.AnimeSeasonComments)
                .HasForeignKey(ac => ac.CommentId);

            modelBuilder.Entity<MangaComment>()
                .HasKey(x => new { x.MangaId, x.CommentId });

            modelBuilder.Entity<MangaComment>()
                .HasOne(mc => mc.Manga)
                .WithMany(m => m.MangaComments)
                .HasForeignKey(mc => mc.MangaId);

            modelBuilder.Entity<MangaComment>()
                .HasOne(mc => mc.Comment)
                .WithMany(c => c.MangaComments)
                .HasForeignKey(mc => mc.CommentId);

            modelBuilder.Entity<PageComment>()
                .HasKey(x => new { x.PageId, x.CommentId });

            modelBuilder.Entity<PageComment>()
                .HasOne(pc => pc.Page)
                .WithMany(p => p.PageComments)
                .HasForeignKey(pc => pc.PageId);

            modelBuilder.Entity<PageComment>()
                .HasOne(pc => pc.Comment)
                .WithMany(c => c.PageComments)
                .HasForeignKey(pc => pc.CommentId);

            modelBuilder.Entity<UserComment>()
                .HasKey(x => new { x.UserId, x.CommentId });

            modelBuilder.Entity<UserComment>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserComments)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserComment>()
                .HasOne(uc => uc.Comment)
                .WithMany(c => c.UserComments)
                .HasForeignKey(uc => uc.CommentId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany()
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}