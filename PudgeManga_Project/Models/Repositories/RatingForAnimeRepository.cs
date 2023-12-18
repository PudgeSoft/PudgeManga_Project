using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;

namespace PudgeManga_Project.Models.Repositories
{
    public class RatingForAnimeRepository : IRatingForAnimeRepository
    {
        private readonly ApplicationDBContext _context;

        public RatingForAnimeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<double> GetAnimeAverageRatingAsync(int animeId)
        {
            var ratings = _context.RatingForAnimes.Where(r => r.AnimeId == animeId);

            if (!ratings.Any())
            {
                return 0;
            }

            return await ratings.AverageAsync(r => r.Value);
        }

        public async Task AddRatingAsync(RatingForAnime rating)
        {
            _context.RatingForAnimes.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRatingAsync(RatingForAnime rating)
        {
            _context.RatingForAnimes.Update(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<RatingForAnime> GetRatingAsync(int animeId, string userId)
        {
            return _context.RatingForAnimes.FirstOrDefault(r => r.UserId == userId && r.AnimeId == animeId);
        }
    }
}
