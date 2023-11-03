using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.ViewModels;

namespace PudgeManga_Project.Models.Repositories
{
    public class AdminMangaRepository : IAdminMangaRepository<Manga, int>
    {
        private readonly ApplicationDBContext context;
        public AdminMangaRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task Delete(Manga manga)
        {
            context.Remove(manga);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Manga>> GetAll()
        {
            return await context.Mangas.ToListAsync();
        }

        public async Task<Manga> GetById(int id)
        {
            return await context.Mangas
                .Include(ch => ch.Chapters)
                .Include(comm => comm.Comments)
                .Include(popul => popul.Popularity)
                .FirstOrDefaultAsync(i => i.MangaId == id);
        }

        public async Task<Manga> Add(Manga entity)
        {
            await context.Mangas.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(Manga entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            context.Mangas.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
