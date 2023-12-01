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
        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();
        }
        public async Task AddGenreAsync(Genre genre)
        {
             await _context.Genres.AddAsync(genre);
             await _context.SaveChangesAsync();
        }
    }
}
