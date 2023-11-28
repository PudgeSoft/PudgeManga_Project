using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels;

namespace PudgeManga_Project.Controllers
{
    public class SearchController : Controller
    {
 
        private readonly IGenreRepository _genreRepository;
        private readonly IMangaRepository<Manga, int> _mangaRepository;
        private readonly ISearchRepository _searchRepository;

        public SearchController(IGenreRepository genreRepository,
            IMangaRepository<Manga, int> mangaRepository,
            ISearchRepository searchRepository)

        {
            _genreRepository = genreRepository;
            _mangaRepository = mangaRepository;
            _searchRepository = searchRepository;
        }


        public async Task<IActionResult> Index()
        {
            var allManga = await _mangaRepository.GetAll();
            var allGenres = await _genreRepository.GetAllGenres();


            var searchViewModel = new SearchViewModel
            {
                Mangas = allManga.ToList(),
                AllGenres = allGenres.Select(genre => new SelectListItem
                {
                    Value = genre.GenreId.ToString(),
                    Text = genre.Name
                }).ToList()
            };

            return View(searchViewModel);
        }

        [ActionName("Index")]
        [HttpGet]
        public async Task<IActionResult> Search(string searchString, List<int> genres)
        {
            var searchAndFilters = await _searchRepository.SearchMangaAsync(searchString, genres);
            var allGenres = await _genreRepository.GetAllGenres();

            var searchViewModel = new SearchViewModel
            {
                Mangas = searchAndFilters.ToList(),
                AllGenres = allGenres.Select(genre => new SelectListItem
                {
                    Value = genre.GenreId.ToString(),
                    Text = genre.Name
                }).ToList()
            };

            return View(searchViewModel);
        }


    }
}
