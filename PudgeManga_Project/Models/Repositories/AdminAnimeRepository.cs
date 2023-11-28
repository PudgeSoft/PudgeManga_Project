using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;

namespace PudgeManga_Project.Models.Repositories
{
    public class AdminAnimeRepository : IAdminAnimeRepository<Anime, int>
    {
        private readonly ApplicationDBContext _context;
        public AdminAnimeRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public Task<Anime> Add(Anime entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Anime entity)
        {
            throw new NotImplementedException();
        }
        
        public async Task<IEnumerable<Anime>> GetAll()
        {
            return await _context.Animes
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.GenreForAnime)
            .ToListAsync();
        }

        public Task<Anime> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Anime entity)
        {
            throw new NotImplementedException();
        }
    }
}
