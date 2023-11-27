using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;

namespace PudgeManga_Project.Models.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDBContext  _context;
        public GenreRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Genre>> GetAllGenres()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}
