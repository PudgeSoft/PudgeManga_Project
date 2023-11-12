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

		public async Task Delete(Chapter chapter)
		{
            _context.Remove(chapter);
            await _context.SaveChangesAsync();
        }


		public async Task<Chapter> GetById(int id)
		{
            return await _context.Chapters
                .Include(p => p.Pages)
                .FirstOrDefaultAsync(i => i.ChapterId == id);
        }

		public async Task Save()
		{
            await _context.SaveChangesAsync();
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
