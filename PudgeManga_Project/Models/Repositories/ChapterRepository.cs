﻿using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;

namespace PudgeManga_Project.Models.Repositories
{
    public class ChapterRepository:IChapterRepository<Chapter,int>
    {
        private readonly ApplicationDBContext _context;
        public ChapterRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Chapter>> GetChaptersForManga(int mangaId)
        {
            var chapters = await _context.Chapters
            .Where(c => c.MangaID == mangaId)
            .ToListAsync();

            return chapters;
        }
        public async Task<int> GetTotalChapters(int mangaId)
        {

            var totalChapters = await _context.Chapters
                .Where(c => c.MangaID == mangaId)
                .CountAsync();

            return totalChapters;
        }
        public async Task<List<Chapter>> GetAllAsync()
        {
            var allChapters = await _context.Chapters.ToListAsync();
            return allChapters;
        }
    }
}
