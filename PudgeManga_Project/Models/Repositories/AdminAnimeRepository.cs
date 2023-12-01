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

        public async Task UpdateAsync(Anime entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var existingEntity = await _context.Animes
                .Include(a => a.AnimeGenres)
                    .ThenInclude(ag => ag.GenreForAnime)
                .FirstOrDefaultAsync(a => a.AnimeId == entity.AnimeId);

            if (existingEntity == null)
            {
                throw new InvalidOperationException($"Anime with ID {entity.AnimeId} not found.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            // Очистити існуючі жанри
            existingEntity.AnimeGenres.Clear();

            foreach (var genreId in entity.AnimeGenres.Select(mg => mg.GenreId))
            {
                existingEntity.AnimeGenres.Add(new AnimeGenre { AnimeId = existingEntity.AnimeId, GenreId = genreId });
            }



            await _context.SaveChangesAsync();
        }


    }
}
