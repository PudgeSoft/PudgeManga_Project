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
                .Include(ch => ch.Chapters)
                .Include(comm => comm.Comments)
                .Include(popul => popul.Popularity)
                .FirstOrDefaultAsync(i => i.MangaId == id);
        }


    }
}
