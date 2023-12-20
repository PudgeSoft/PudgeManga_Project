using FluentAssertions.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels.AdminAnimeViewModels;
using PudgeManga_Project.ViewModels.AdminAnimeViewModels.AdminSeasonsViewModels;
using PudgeManga_Project.ViewModels.AdminMangaViewModels;
using PudgeManga_Project.ViewModels.AdminMangaViewModels.AdminChaptersViewModels;
using System.Data;

namespace PudgeManga_Project.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminAnimeController : Controller
    {
        private readonly IAdminAnimeRepository<Anime, int> _adminAnimeRepository;
        private readonly IGoogleDriveAPIRepository<IFormFile> _googleDriveAPIRepository;
        private readonly IAnimeGenreRepository _animeGenreRepository;
        private readonly IAdminSeasonRepository<AnimeSeason, int> _adminSeasonRepository;
        public AdminAnimeController(IGoogleDriveAPIRepository<IFormFile> googleDriveAPIRepository,
            IAnimeGenreRepository animeGenreRepository,
            IAdminAnimeRepository<Anime, int> adminAnimeRepository,
            IAdminSeasonRepository<AnimeSeason,int> adminSeasonRepository)
        {
            _adminAnimeRepository = adminAnimeRepository;
            _googleDriveAPIRepository = googleDriveAPIRepository;
            _animeGenreRepository = animeGenreRepository;
            _adminSeasonRepository = adminSeasonRepository;
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
        public async Task<IActionResult> Edit(int animeId)
        {
            var anime = await _adminAnimeRepository.GetByIdAsync(animeId);
            if (anime == null)
            {
                return NotFound();
            }

            var allGenres = await _animeGenreRepository.GetAllGenresAsync();

            var editAnimeViewModel = new EditAnimeViewModel
            {
                AnimeId = anime.AnimeId,
                Title = anime.Title,
                Description = anime.Description,
                ImageUrl = anime.ImageUrl,
                Director = anime.Director,
                Studio = anime.Studio,
                Dubbing = anime.Dubbing,
                Type = anime.Type,
                ReleaseYear = anime.ReleaseYear,
                AnimeGenreIds = anime.AnimeGenres.Select(ag => ag.GenreId).ToList(),
                AllGenres = allGenres.Select(genre => new SelectListItem
                {
                    Value = genre.AnimeGenreId.ToString(),
                    Text = genre.Name
                }).ToList()
            };

            return View(editAnimeViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int animeId, EditAnimeViewModel editAnimeViewModel)
        {

            List<int> selectedGenreIds = editAnimeViewModel.AnimeGenreIds;
            var anime = new Anime
            {
                AnimeId = editAnimeViewModel.AnimeId,
                Title = editAnimeViewModel.Title,
                Description= editAnimeViewModel.Description,
                ImageUrl= editAnimeViewModel.ImageUrl,
                Director= editAnimeViewModel.Director,
                Studio= editAnimeViewModel.Studio,
                Dubbing= editAnimeViewModel.Dubbing,
                Type= editAnimeViewModel.Type,
                ReleaseYear= editAnimeViewModel.ReleaseYear,
                AnimeGenres = selectedGenreIds
                .Select(genreId => new AnimeGenre { GenreId = genreId }).ToList()
            };

            await _adminAnimeRepository.UpdateAsync(anime);


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
        public async Task<IActionResult> CreateGenre()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre(GenreForAnime genre)
        {

            await _animeGenreRepository.AddGenreAsync(genre);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Seasons(int animeId)
        {
            ViewData["AnimeId"] = animeId;
            var seasons = await _adminSeasonRepository.GetSeasonsForAnimeAsync(animeId);
            return View(seasons);
        }
        public async Task<IActionResult> CreateSeason(int animeId)
        {
            var viewModel = new CreateSeasonViewModel
            {
                AnimeId = animeId
            };
            ViewData["AnimeId"] = animeId;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeason(CreateSeasonViewModel seasonViewModel)
        {
            if (ModelState.IsValid)
            {
                var season = new AnimeSeason
                {
                    SeasonNumber = seasonViewModel.SeasonNumber,
                    Title = seasonViewModel.Title
                };

                await _adminSeasonRepository.AddSeasonsToAnimeAsync(seasonViewModel.AnimeId, season);

                return RedirectToAction("Seasons", new { AnimeId = seasonViewModel.AnimeId });
            }

            return View(seasonViewModel);
        }
        public async Task<IActionResult> EditSeason(int seasonId)
        {
            var season = await _adminSeasonRepository.GetByIdAsync(seasonId);
            if (season == null)
            {
                return NotFound();
            }
            ViewData["AnimeId"] = season.AnimeId;
            return View(season);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSeason(int seasonId, EditSeasonViewModel editSeasonViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Помилка при редагуванні сезону");
                return View(editSeasonViewModel);
            }

            var chapter = await _adminSeasonRepository.GetByIdAsync(seasonId);

            if (chapter == null)
            {
                return NotFound();
            }

            chapter.SeasonNumber = editSeasonViewModel.SeasonNumber;
            chapter.Title = editSeasonViewModel.Title;

            await _adminSeasonRepository.UpdateAsync(chapter);
            ;
            return RedirectToAction("Seasons", new { AnimeId = chapter.AnimeId });
        }

        public async Task<IActionResult> DeleteSeason(int seasonId)
        {
            var season = await _adminSeasonRepository.GetByIdAsync(seasonId);
            if (season == null)
            {
                return NotFound();
            }
            ViewData["AnimeId"] = season.AnimeId;
            return View(season);
        }

        [HttpPost, ActionName("DeleteSeason")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedSeason(int seasonId)
        {
            var season = await _adminSeasonRepository.GetByIdAsync(seasonId);
            if (season == null)
            {
                return View("Delete error");
            }
            ViewData["AnimeId"] = season.AnimeId;
            await _adminSeasonRepository.DeleteAsync(season);
            return RedirectToAction("Seasons", new { AnimeId = season.AnimeId });
        }
        public async Task<IActionResult> AddSeries(int seasonId)
        {
            var episode = await _adminSeasonRepository.GetByIdAsync(seasonId);
            if (episode == null)
            {
                return NotFound();
            }
            ViewData["seasonId"] = seasonId;
            return View();
        }

        [HttpPost, ActionName("AddSeries")]
        public async Task<IActionResult> Upload(IFormFile file, int seasonId)
        {
            try
            {
                var season = await _adminSeasonRepository.GetByIdAsync(seasonId);
                if (season == null)
                {
                    return NotFound();
                }
                var folderName = $"{seasonId}{season.Title}";
                string folderId = _googleDriveAPIRepository.GetOrCreateFolder(folderName);
                using (var fileStream = file.OpenReadStream())
                {
                    // Завантажити файл на Google Drive
                    _googleDriveAPIRepository.UploadFileStreamToGoogleDrive(fileStream, "test",folderId);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при завантаженні файлу на Google Drive: {ex.Message}");
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ButtonClick(int seasonId)
        {
            var season = await _adminSeasonRepository.GetByIdAsync(seasonId);
            string folderName = $"{seasonId}{season.Title}";

            string folderId = _googleDriveAPIRepository.GetOrCreateFolder(folderName);
            var modifiedPhotoLinks = _googleDriveAPIRepository.GetModifiedFileLinks(folderId);
            await _googleDriveAPIRepository.AddFileLinksToAnimeEpisodesWithSeasons(modifiedPhotoLinks, seasonId);

            return RedirectToAction("Index");
        }

    }
}
