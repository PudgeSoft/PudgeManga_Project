using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;

namespace PudgeManga_Project.Models.Repositories
{
    public class ChapterRepository:IChapterRepository<Chapter,int>
    {
        private readonly ApplicationDBContext context;
        public ChapterRepository(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Chapter>> GetChaptersForManga(int mangaId)
        {
            var chapters = await context.Chapters
            .Where(c => c.MangaID == mangaId)
            .ToListAsync();

            return chapters;
        }
    }
}
