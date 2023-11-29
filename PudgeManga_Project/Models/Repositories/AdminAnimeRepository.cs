using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using System;

namespace PudgeManga_Project.Models.Repositories
{
    public class AdminAnimeRepository : IAdminAnimeRepository<Anime, int>
    {
        private readonly ApplicationDBContext _context;
        public AdminAnimeRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Anime> AddAsync(Anime entity)
        {
            await _context.Animes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Anime entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync(); ;
        }
        
        public async Task<IEnumerable<Anime>> GetAllAsync()
        {
            return await _context.Animes
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.GenreForAnime)
            .ToListAsync();
        }

        public async Task<Anime> GetByIdAsync(int id)
        {
            return await _context.Animes
                .Include(m => m.AnimeGenres)
                    .ThenInclude(ag => ag.GenreForAnime)
                .Include(ch => ch.AnimeSeasons)
                .FirstOrDefaultAsync(i => i.AnimeId == id);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Anime entity)
        {
            throw new NotImplementedException();
        }
    }
}
