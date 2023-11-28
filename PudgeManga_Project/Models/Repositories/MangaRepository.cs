﻿using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.ViewModels;


namespace PudgeManga_Project.Models.Repositories
{
    public class MangaRepository:IMangaRepository<Manga,int>
    {
        private readonly ApplicationDBContext _context;
        public MangaRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manga>> GetAll()
        {
            return await _context.Mangas.ToListAsync();
        }

        public async Task<Manga> GetById(int id)
        {
            return await _context.Mangas
                .Include(m => m.MangaGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(ch => ch.Chapters)
                .Include(comm => comm.Comments)
                .Include(popul => popul.Popularity)
                .FirstOrDefaultAsync(i => i.MangaId == id);
        }


        public async Task<Manga> GetByIdComments(int id)
        {
            return await _context.Mangas
                .Include(comm => comm.Comments)
                .FirstOrDefaultAsync(i => i.MangaId == id);
        }
        public async Task<Manga> GetByIdReading(int id, int chapterNumber)
        {
            return await _context.Mangas
                .Include(ch => ch.Chapters)
                .ThenInclude(p => p.Pages)
                .Where(m => m.MangaId == id) 
                .Where(ch => ch.Chapters.Any(c => c.ChapterNumber == chapterNumber))
                .FirstOrDefaultAsync(i => i.MangaId == id);
        }


    }
}
