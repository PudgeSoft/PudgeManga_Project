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

        public async Task<Anime> GetByIdAsync(int id)
        {
            return await _context.Animes
            .Include(m => m.AnimeGenres)
                .ThenInclude(ag => ag.GenreForAnime)
            .Include(ch => ch.AnimeSeasons)
            .FirstOrDefaultAsync(i => i.AnimeId == id);
        }

    }
}
