using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Data.Enum;
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


        public async Task<IEnumerable<Manga>> SearchMangaAsync(string searchString, List<int> genres, SearchType searchType)
        {
            var mangaQuery = _context.Mangas
                .Include(m => m.MangaGenres)
                .ThenInclude(mg => mg.Genre)
                .AsQueryable(); 

            if (searchType == SearchType.Manga)
            {
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
            }
            return await mangaQuery.ToListAsync();
        }

        public async Task<IEnumerable<Anime>> SearchAnimeAsync(string searchString, List<int> genres, SearchType searchType)
        {
            var animeQuery = _context.Animes
                .Include(a => a.AnimeGenres)
                .ThenInclude(ag => ag.GenreForAnime)
                .AsQueryable();

            if (searchType == SearchType.Anime)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    animeQuery = animeQuery.Where(a => a.Title.Contains(searchString));
                }
                if (genres != null && genres.Any())
                {
                    animeQuery = animeQuery.Where(a => a.AnimeGenres
                        .Select(ag => ag.GenreForAnime.AnimeGenreId)
                        .Any(genreId => genres.Contains(genreId)));
                }
            }

            return await animeQuery.ToListAsync();
        }
    }
}
