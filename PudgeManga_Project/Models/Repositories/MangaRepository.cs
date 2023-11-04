using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.ViewModels;


namespace PudgeManga_Project.Models.Repositories
{
    public class MangaRepository:IMangaRepository<Manga,int>
    {
        private readonly ApplicationDBContext context;
        public MangaRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Manga>> GetAll()
        {
            return await context.Mangas.ToListAsync();
        }

        public async Task<Manga> GetById(int id)
        {
            return await context.Mangas
                .FirstOrDefaultAsync(i => i.MangaId == id);
        }

        public async Task<Manga> GetByIdChapters(int id)
        {
            return await context.Mangas
                .Include(ch => ch.Chapters)
                .FirstOrDefaultAsync(i => i.MangaId == id);
        }
        public async Task<Manga> GetByIdComments(int id)
        {
            return await context.Mangas
                .Include(comm => comm.Comments)
                .FirstOrDefaultAsync(i => i.MangaId == id);
        }
        public async Task<Manga> GetByIdReading(int id)
        {
            return await context.Mangas
                .Include(ch => ch.Chapters)
                .ThenInclude(p => p.Pages)
                .FirstOrDefaultAsync(i => i.MangaId == id);
        }


    }
}
