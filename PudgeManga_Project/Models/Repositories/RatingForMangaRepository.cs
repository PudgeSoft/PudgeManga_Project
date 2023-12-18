using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;

namespace PudgeManga_Project.Models.Repositories
{
    public class RatingForMangaRepository : IRatingForMangaRepository
    {
        private readonly ApplicationDBContext _context;

        public RatingForMangaRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<double> GetMangaAverageRatingAsync(int mangaId)
        {
            var ratings = _context.RatingForMangas.Where(r => r.MangaId == mangaId);

            if (!ratings.Any())
            {
                return 0;
            }

            return await ratings.AverageAsync(r => r.Value);
        }

        public async Task AddRatingAsync(RatingForManga rating)
        {
            _context.RatingForMangas.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRatingAsync(RatingForManga rating)
        {
            _context.RatingForMangas.Update(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<RatingForManga> GetRatingAsync(int mangaId, string userId)
        {
            return await _context.RatingForMangas.FirstOrDefaultAsync(r => r.UserId == userId && r.MangaId == mangaId);
        }
    }
}
