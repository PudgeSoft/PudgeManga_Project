using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.ViewModels;

namespace PudgeManga_Project.Models.Repositories
{
    public class AdminMangaRepository : IAdminMangaRepository<Manga, int>
    {
        private readonly ApplicationDBContext _context;
        public AdminMangaRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task Delete(Manga manga)
        {
            _context.Remove(manga);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Manga>> GetAll()
        {
            return await _context.Mangas
            .Include(m => m.MangaGenres)
                .ThenInclude(mg => mg.Genre)
            .ToListAsync();
        }
        public async Task<Manga> GetById(int id)
        {
            return await _context.Mangas
                .Include(m => m.MangaGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(ch => ch.Chapters)
                .Include(rat => rat.Ratings)
                .FirstOrDefaultAsync(i => i.MangaId == id);
        }

        public async Task<Manga> Add(Manga entity)
        {
            await _context.Mangas.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(Manga entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var existingEntity = await _context.Mangas
                .Include(m => m.MangaGenres)
                    .ThenInclude(mg => mg.Genre)
                .FirstOrDefaultAsync(m => m.MangaId == entity.MangaId);

            if (existingEntity == null)
            {
                throw new InvalidOperationException($"Manga with ID {entity.MangaId} not found.");
            }


            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            existingEntity.MangaGenres.Clear();

            foreach (var genreId in entity.MangaGenres.Select(mg => mg.GenreId))
            {
                existingEntity.MangaGenres.Add(new MangaGenre { MangaId = existingEntity.MangaId, GenreId = genreId });
            }

            await _context.SaveChangesAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
