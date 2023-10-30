﻿using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;

namespace PudgeManga_Project.Models.Repositories
{
    public class MangaRepository : IRepository<Manga, int>
    {
        private readonly ApplicationDBContext context;
        public MangaRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task Delete(int id)
        {
            var manga = await context.Mangas.FirstOrDefaultAsync(b=> b.MangaId == id);
            if(manga != null)
            {
                context.Remove(manga);
            }
        }

        public async Task<IEnumerable<Manga>> GetAll()
        {
            return await context.Mangas.Include(b => b.Author).ToListAsync();
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

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
