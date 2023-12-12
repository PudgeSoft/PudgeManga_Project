using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using PudgeManga_Project.Data;
using PudgeManga_Project.Data.Enum;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels;
using PudgeManga_Project.ViewModels.SearchViewModels;

namespace PudgeManga_Project.Controllers
{
    public class SearchController : Controller
    {

        private readonly IGenreRepository _genreRepository;
        private readonly IMangaRepository<Manga, int> _mangaRepository;
        private readonly ISearchRepository _searchRepository;
        private readonly IAnimeRepository<Anime, int> _animeRepository;
        private readonly IAnimeGenreRepository _animeGenreRepository;

        public SearchController(IGenreRepository genreRepository,
            IMangaRepository<Manga, int> mangaRepository,
            ISearchRepository searchRepository,
            IAnimeRepository<Anime, int> animeRepository,
            IAnimeGenreRepository animeGenreRepository)

        {
            _genreRepository = genreRepository;
            _mangaRepository = mangaRepository;
            _searchRepository = searchRepository;
            _animeRepository = animeRepository;
            _animeGenreRepository = animeGenreRepository;
        }


        public async Task<IActionResult> Index()
        {
            var allManga = await _mangaRepository.GetAllAsync();
            var allAnime = await _animeRepository.GetAllAsync();
            var allGenresMangas = await _genreRepository.GetAllGenresAsync();
            var allGenresAnimes = await _animeGenreRepository.GetAllGenresAsync();


            var searchViewModel = new SearchMangaAndAnimeViewModel
            {
                Mangas = allManga.ToList(),
                AllGenresMangas = allGenresMangas.Select(genre => new SelectListItem
                {
                    Value = genre.GenreId.ToString(),
                    Text = genre.Name
                }).ToList(),
                Animes = allAnime.ToList(),
                AllGenresAnimes = allGenresAnimes.Select(item => new SelectListItem
                {
                    Value = item.AnimeGenreId.ToString(),
                    Text = item.Name
                }).ToList()
            };


            return View(searchViewModel);
        }

        [ActionName("Index")]
        [HttpGet]
        public async Task<IActionResult> Search(string searchString, List<int> genres, SearchType searchType)
        {

            if (searchType == SearchType.Manga)
            {
                var searchMangaAndFilters = await _searchRepository.SearchMangaAsync(searchString, genres, searchType);
                var allGenresMangas = await _genreRepository.GetAllGenresAsync();

                var searchViewModel = new SearchMangaAndAnimeViewModel
                {
                    Mangas = searchMangaAndFilters.ToList(),
                    AllGenresMangas = allGenresMangas.Select(genre => new SelectListItem
                    {
                        Value = genre.GenreId.ToString(),
                        Text = genre.Name
                    }).OrderBy(item => item.Text).ToList(),
                    SearchType = searchType
                };
                return View(searchViewModel);
            }
            else
            {
                var searchAnimeAndFilters = await _searchRepository.SearchAnimeAsync(searchString, genres, searchType);
                var allGenresAnimes = await _animeGenreRepository.GetAllGenresAsync();

                var searchViewModel = new SearchMangaAndAnimeViewModel
                {
                    Animes = searchAnimeAndFilters.ToList(),
                    AllGenresAnimes = allGenresAnimes.Select(genre => new SelectListItem
                    {
                        Value = genre.AnimeGenreId.ToString(),
                        Text = genre.Name
                    }).OrderBy(item => item.Text).ToList(),
                    SearchType = searchType,
                };
                return View(searchViewModel);
            }
        }
    }
}
