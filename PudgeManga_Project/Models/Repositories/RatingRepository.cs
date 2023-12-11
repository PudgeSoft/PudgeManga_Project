using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;

namespace PudgeManga_Project.Models.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDBContext _context;

        public RatingRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<double> GetAnimeAverageRatingAsync(int animeId)
        {

            var ratings = _context.Ratings.Where(r => r.AnimeId == animeId);

            if (!ratings.Any())
            {
                return 0;
            }

            return await ratings.AverageAsync(r => r.Value);
        }

        public async Task<double> GetMangaAverageRatingAsync(int mangaId)
        {
            var ratings = _context.Ratings.Where(r => r.MangaId == mangaId);

            if (!ratings.Any())
            {
                return 0;
            }

            return await ratings.AverageAsync(r => r.Value);
        }

        public async Task AddRatingAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
        }
    }
}
