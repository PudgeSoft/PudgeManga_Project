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
            return await _context.Ratings
                .Where(r => r.AnimeId == animeId)
                .AverageAsync(r => r.Value);
        }

        public async Task<double> GetMangaAverageRatingAsync(int mangaId)
        {
            return await _context.Ratings
                .Where(r => r.MangaId == mangaId)
                .AverageAsync(r => r.Value);
        }

        //public async Task<int> GetAnimeRating(int userId, int animeId)
        //{
        //    var rating = await _context.Ratings
        //    .Where(r => r.AnimeId == animeId)
        //    .FirstOrDefaultAsync();

        //    return rating.Value;
        //}

        //public async Task<int> GetMangaRating(int userId, int mangaId)
        //{
        //    var rating = await _context.Ratings
        //    .Where(r => r.MangaId == mangaId)
        //    .FirstOrDefaultAsync();

        //    return rating.Value;
        //}
    }
}
