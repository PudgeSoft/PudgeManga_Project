using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels.AdminAnimeViewModels;
using PudgeManga_Project.ViewModels.AdminMangaViewModels;

namespace PudgeManga_Project.Controllers
{
    public class AdminAnimeController : Controller
    {
        private readonly IAdminAnimeRepository<Anime, int> _adminAnimeRepository;
        private readonly IGoogleDriveAPIRepository<IFormFile> _googleDriveAPIRepository;
        private readonly IAnimeGenreRepository _animeGenreRepository;
        public AdminAnimeController(IGoogleDriveAPIRepository<IFormFile> googleDriveAPIRepository,
            IAnimeGenreRepository animeGenreRepository,
            IAdminAnimeRepository<Anime, int> adminAnimeRepository)
        {
            _adminAnimeRepository = adminAnimeRepository;
            _googleDriveAPIRepository = googleDriveAPIRepository;
            _animeGenreRepository = animeGenreRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _adminAnimeRepository.GetAllAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var allGenres = await _animeGenreRepository.GetAllGenresAsync();

            var createAnimeViewModel = new CreateAnimeViewModel
            {
                AllGenres = allGenres.Select(genre => new SelectListItem
                {
                    Value = genre.AnimeGenreId.ToString(),
                    Text = genre.Name
                }).ToList()
            };

            return View(createAnimeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAnimeViewModel animeViewModel)
        {

            var anime = new Anime
            {
                Title = animeViewModel.Title,
                Description = animeViewModel.Description,
                ImageUrl = animeViewModel.ImageUrl,
                Director = animeViewModel.Director,
                Studio = animeViewModel.Studio,
                Dubbing = animeViewModel.Dubbing,
                Type = animeViewModel.Type,
                ReleaseYear = animeViewModel.ReleaseYear,
                AnimeGenres = animeViewModel.AnimeGenreIds.Select(genreId => new AnimeGenre { GenreId = genreId }).ToList()
            };

            await _adminAnimeRepository.AddAsync(anime);

            var allGenres = await _animeGenreRepository.GetAllGenresAsync();

            animeViewModel.AllGenres = allGenres.Select(genre => new SelectListItem
            {
                Value = genre.AnimeGenreId.ToString(),
                Text = genre.Name
            }).ToList();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int animeId)
        {
            var anime = await _adminAnimeRepository.GetByIdAsync(animeId);
            if (anime == null)
            {
                return NotFound();
            }
            return View(anime);
        }

        // POST: Mangas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int animeId)
        {
            var anime = await _adminAnimeRepository.GetByIdAsync(animeId);
            if (anime == null)
            {
                return View("Delete error");
            }
            await _adminAnimeRepository.DeleteAsync(anime);
            await _adminAnimeRepository.SaveAsync();
            return RedirectToAction("Index");
        }

    }
}
