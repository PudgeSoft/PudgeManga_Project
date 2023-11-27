using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using System;

namespace PudgeManga_Project.Models.Repositories
{
	public class AdminChapterRepository : IAdminChapterRepository<Chapter, int>
	{
        private readonly ApplicationDBContext _context;
        public AdminChapterRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Chapter> Add(Chapter chapter)
		{
            await _context.Chapters.AddAsync(chapter);
            await _context.SaveChangesAsync();
            return chapter;
        }
        public async Task AddChapterToMangaAsync(int mangaId, Chapter chapter)
        {
            var manga = await _context.Mangas
            .Include(m => m.Chapters) 
            .FirstOrDefaultAsync(m => m.MangaId == mangaId);

            if (manga != null)
            {
                manga.Chapters.Add(chapter);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Manga with ID {mangaId} not found.");
            }
        }

        public async Task<IEnumerable<Chapter>> GetChaptersForManga(int mangaId)
        {
            var chapters = await _context.Chapters
            .Where(c => c.MangaID == mangaId)
            .ToListAsync();

            return chapters;
        }
        public async Task Delete(Chapter chapter)
		{
            _context.Remove(chapter);
            await _context.SaveChangesAsync();
        }


		public async Task<Chapter> GetById(int chapterId)
		{
            return await _context.Chapters
                .Include(p => p.Pages)
                .FirstOrDefaultAsync(i => i.ChapterId == chapterId);
        }


		public async Task UpdateAsync(Chapter chapter)
		{
            if (chapter == null)
            {
                throw new ArgumentNullException(nameof(chapter));
            }
            _context.Chapters.Update(chapter);
            await _context.SaveChangesAsync();
        }


    }
}
