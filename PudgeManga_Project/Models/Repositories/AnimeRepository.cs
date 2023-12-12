using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;

namespace PudgeManga_Project.Models.Repositories
{

    public class AnimeRepository : IAnimeRepository<Anime, int>
    {
        private readonly ApplicationDBContext _context;

        public AnimeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Anime>> GetAllAsync()
        {
            return await _context.Animes.ToListAsync();
        }

        public async Task<Anime> GetAnimeByIdAsync(int animeId)
        {
            return await _context.Animes
                .Include(m => m.AnimeGenres)
                .ThenInclude(ag => ag.GenreForAnime)
                .FirstOrDefaultAsync(a => a.AnimeId == animeId);
        }

        public async Task< List<AnimeSeason>> GetSeasonsByAnimeIdAsync(int animeId)
        {
            return await _context.AnimeSeasons
                .Where(s => s.AnimeId == animeId)
                .ToListAsync();
        }

        public async Task<List<AnimeEpisode>> GetEpisodesBySeasonIdAsync(int seasonId)
        {
            return await  _context.AnimesEpisodes
                .Where(e => e.SeasonId == seasonId)
                .ToListAsync();
        }

    }
}
