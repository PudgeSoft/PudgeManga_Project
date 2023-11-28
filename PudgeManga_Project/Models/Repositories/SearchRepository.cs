using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using System.Linq;

namespace PudgeManga_Project.Models.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ApplicationDBContext _context;

        public SearchRepository(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Manga>> SearchMangaAsync(string searchString, List<int> genres)
        {
            var mangaQuery = _context.Mangas
                .Include(m => m.MangaGenres)
                .ThenInclude(mg => mg.Genre)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                mangaQuery = mangaQuery.Where(m => m.Title.Contains(searchString));
            }

            if (genres != null && genres.Any())
            {
                mangaQuery = mangaQuery.Where(m => m.MangaGenres
                    .Select(mg => mg.Genre.GenreId)
                    .Any(genreId => genres.Contains(genreId)));
            }

            return await mangaQuery.ToListAsync();
        }

    }
}
