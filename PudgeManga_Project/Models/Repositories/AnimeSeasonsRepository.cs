using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;

namespace PudgeManga_Project.Models.Repositories
{
    public class AnimeSeasonsRepository : IAnimeSeasonsRepository<AnimeSeason, int>
    {
        private readonly ApplicationDBContext _context;
        public AnimeSeasonsRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AnimeSeason>> GetSeasonsForAnimeAsync(int animeId)
        {
            var seasons = await _context.AnimeSeasons
            .Where(c => c.AnimeId == animeId)
            .ToListAsync();

            return seasons;
        }

        public Task<int> GetTotalAnimeSeasonsAsync(int animeId)
        {
            throw new NotImplementedException();
        }
    }
}
