using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;

namespace PudgeManga_Project.Models.Repositories
{
    public class AdminSeasonRepository : IAdminSeasonRepository<AnimeSeason, int>
    {
        private readonly ApplicationDBContext _context;
        public AdminSeasonRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<AnimeSeason> AddAsync(AnimeSeason season)
        {
            await _context.AnimeSeasons.AddAsync(season);
            await _context.SaveChangesAsync();
            return season;
        }
        public async Task AddSeasonsToAnimeAsync(int seasonId, AnimeSeason season)
        {
            var manga = await _context.Animes
            .Include(a =>a.AnimeSeasons)
            .FirstOrDefaultAsync(m => m.AnimeId == seasonId);

            if (manga != null)
            {
                manga.AnimeSeasons.Add(season);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Anime with ID {seasonId} not found.");
            }
        }

        public async Task<IEnumerable<AnimeSeason>> GetSeasonsForAnimeAsync(int animeId)
        {
            var chapters = await _context.AnimeSeasons
            .Where(c => c.AnimeId == animeId)
            .ToListAsync();

            return chapters;
        }
        public async Task DeleteAsync(AnimeSeason season)
        {
            _context.Remove(season);
            await _context.SaveChangesAsync();
        }


        public async Task<AnimeSeason> GetByIdAsync(int seasonId)
        {
            return await _context.AnimeSeasons
                .Include(e => e.AnimeEpisodes)
                .FirstOrDefaultAsync(i => i.AnimeSeasonId == seasonId);
        }


        public async Task UpdateAsync(AnimeSeason season)
        {
            if (season == null)
            {
                throw new ArgumentNullException(nameof(season));
            }
            _context.AnimeSeasons.Update(season);
            await _context.SaveChangesAsync();
        }
    }
}
