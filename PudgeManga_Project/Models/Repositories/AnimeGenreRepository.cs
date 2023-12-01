using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PudgeManga_Project.Models.Repositories
{
    public class AnimeGenreRepository : IAnimeGenreRepository
    {
        private readonly ApplicationDBContext _context;
        public AnimeGenreRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<GenreForAnime>> GetAllGenresAsync()
        {
            return await _context.GenresForAnimes.OrderBy(g => g.Name).ToListAsync();
        }
        public async Task AddGenreAsync(GenreForAnime genre)
        {
            await _context.GenresForAnimes.AddAsync(genre);
            await _context.SaveChangesAsync();
        }
    }
}
