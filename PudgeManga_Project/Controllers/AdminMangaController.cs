using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Data;
using PudgeManga_Project.Models;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels.AdminMangaViewModels;
using PudgeManga_Project.ViewModels.AdminMangaViewModels.AdminChaptersViewModels;
using PudgeManga_Project.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace PudgeManga_Project.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminMangaController : Controller
    {
        private readonly IAdminMangaRepository<Manga, int> _AdminMangaRepository;
        private readonly IAdminChapterRepository<Chapter, int> _AdminChapterRepository;
        private readonly IGoogleDriveAPIRepository<IFormFile> _googleDriveAPIRepository;
        private readonly IGenreRepository _genreRepository;
        public AdminMangaController(IAdminMangaRepository<Manga, int> adminMangaRepository,
            IAdminChapterRepository<Chapter, int> adminChapterRepository,
            IChapterRepository<Chapter, int> chapterRepository,
            IGoogleDriveAPIRepository<IFormFile> googleDriveAPIRepository,
            IGenreRepository genreRepository)
        {
            _AdminMangaRepository = adminMangaRepository;
            _AdminChapterRepository = adminChapterRepository;
            _googleDriveAPIRepository = googleDriveAPIRepository;
            _genreRepository = genreRepository;
        }

        // GET: Mangas
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var model = await _AdminMangaRepository.GetAll();
            return View(model);
        }


        // GET: Mangas/Details/5

        //[Authorize(Roles = "admin")]

        public async Task<IActionResult> Details(int id)
        {
            var manga = await _AdminMangaRepository.GetById(id);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }   

        // GET: Mangas/Create
       // [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create()
        {
            var allGenres = await _genreRepository.GetAllGenresAsync();

            var createMangaViewModel = new CreateMangaViewModel
            {
                AllGenres = allGenres.Select(genre => new SelectListItem
                {
                    Value = genre.GenreId.ToString(),
                    Text = genre.Name
                }).ToList()
            };

            return View(createMangaViewModel);
        }

        // POST: Mangas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMangaViewModel mangaViewModel)
        {

            var manga = new Manga
            {
                Title = mangaViewModel.Title,
                Author = mangaViewModel.Author,
                Description = mangaViewModel.Description,
                CoverUrl = mangaViewModel.CoverUrl,
                Type = mangaViewModel.Type,
                Publish = mangaViewModel.Publish,
                Artist = mangaViewModel.Artist,
                Translator = mangaViewModel.Translator,
                MangaGenres = mangaViewModel.GenreIds.Select(genreId => new MangaGenre { GenreId = genreId }).ToList()
            };

            await _AdminMangaRepository.Add(manga);


            var allGenres = await _genreRepository.GetAllGenresAsync();
            mangaViewModel.AllGenres = allGenres.Select(genre => new SelectListItem
            {
                Value = genre.GenreId.ToString(),
                Text = genre.Name
            }).ToList();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CreateGenre()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre(Genre genre)
        {
           
            await _genreRepository.AddGenreAsync(genre);

            return RedirectToAction("Index");
        }

        // GET: Mangas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var manga = await _AdminMangaRepository.GetById(id);
            if (manga == null)
            {
                return NotFound();
            }
            var allGenres = await _genreRepository.GetAllGenresAsync();

            var editMangaViewModel = new EditMangaViewModel
            {
                MangaId = manga.MangaId,
                Title = manga.Title,
                Author = manga.Author,
                Description = manga.Description,
                CoverUrl = manga.CoverUrl,
                Type = manga.Type,
                Publish = manga.Publish,
                Artist = manga.Artist,
                Translator = manga.Translator,
                GenreIds = manga.MangaGenres.Select(mg => mg.GenreId).ToList(),
                AllGenres = allGenres.Select(genre => new SelectListItem
                {
                    Value = genre.GenreId.ToString(),
                    Text = genre.Name
                }).ToList()
            };

            return View(editMangaViewModel);
        }

        // POST: Mangas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditMangaViewModel editMangaViewModel)
        {


            List<int> selectedGenreIds = editMangaViewModel.GenreIds;
            var manga = new Manga
            {
                MangaId = editMangaViewModel.MangaId,
                Title = editMangaViewModel.Title,
                Author = editMangaViewModel.Author,
                Description = editMangaViewModel.Description,
                CoverUrl = editMangaViewModel.CoverUrl,
                Type = editMangaViewModel.Type,
                Publish = editMangaViewModel.Publish,
                Artist = editMangaViewModel.Artist,
                Translator = editMangaViewModel.Translator,
                MangaGenres = selectedGenreIds
                .Select(genreId => new MangaGenre { GenreId = genreId }).ToList()
            };

            await _AdminMangaRepository.UpdateAsync(manga);


            return RedirectToAction("Index");

        }

        // GET: Mangas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var manga = await _AdminMangaRepository.GetById(id);
            if (manga == null)
            {
                return NotFound();
            }
            return View(manga);
        }

        // POST: Mangas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manga = await _AdminMangaRepository.GetById(id);
            if (manga == null)
            {
                return View("Delete error");
            }
            await _AdminMangaRepository.Delete(manga);
            await _AdminMangaRepository.Save();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Chapters(int mangaId)
        {
            ViewData["MangaId"] = mangaId;
            var chapters = await _AdminChapterRepository.GetChaptersForManga(mangaId);
            return View(chapters);
        }
        public async Task<IActionResult> CreateChapter(int mangaId)
        {
            var viewModel = new CreateChapterViewModel
            {
                MangaId = mangaId
            };
            ViewData["MangaId"] = mangaId;
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateChapter(CreateChapterViewModel chapterViewModel)
        {
            if (ModelState.IsValid)
            {
                var chapter = new Chapter
                {
                    ChapterNumber = chapterViewModel.ChapterNumber,
                    Title = chapterViewModel.Title,
                    PublicationDate = chapterViewModel.PublicationDate,
                    Url = chapterViewModel.Url,

                };

                await _AdminChapterRepository.AddChapterToMangaAsync(chapterViewModel.MangaId, chapter);

                return RedirectToAction("Chapters", new { mangaId = chapterViewModel.MangaId });
            }

            return View(chapterViewModel);
        }


        public async Task<IActionResult> EditChapter(int chapterId)
        {
            var chapter = await _AdminChapterRepository.GetById(chapterId);
            if (chapter == null)
            {
                return NotFound();
            }
            ViewData["MangaId"] = chapter.MangaID;
            return View(chapter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditChapter(int chapterId, EditChapterViewModel editChapterViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Помилка при редагуванні глави");
                return View(editChapterViewModel);
            }

            var chapter = await _AdminChapterRepository.GetById(chapterId);

            if (chapter == null)
            {
                return NotFound();
            }

            chapter.Title = editChapterViewModel.Title;
            chapter.ChapterNumber = editChapterViewModel.ChapterNumber;
            chapter.PublicationDate = editChapterViewModel.PublicationDate;
            chapter.Url = editChapterViewModel.Url;

            await _AdminChapterRepository.UpdateAsync(chapter);
            ;
            return RedirectToAction("Chapters", new { mangaId = chapter.MangaID });
        }

        public async Task<IActionResult> DeleteChapter(int chapterId)
        {
            var chapter = await _AdminChapterRepository.GetById(chapterId);
            if (chapter == null)
            {
                return NotFound();
            }
            ViewData["MangaId"] = chapter.MangaID;
            return View(chapter);
        }

        [HttpPost, ActionName("DeleteChapter")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedChapter(int chapterId)
        {
            var chapter = await _AdminChapterRepository.GetById(chapterId);
            if (chapter == null)
            {
                return View("Delete error");
            }
            ViewData["MangaId"] = chapter.MangaID;
            await _AdminChapterRepository.Delete(chapter);
            return RedirectToAction("Chapters", new { mangaId = chapter.MangaID });
        }
        public async Task<IActionResult> AddPages(int chapterId)
        {
            var chapter = await _AdminChapterRepository.GetById(chapterId);
            if (chapter == null)
            {
                return NotFound();
            }
            ViewData["ChapterId"] = chapterId;
            return View();
        }

        [HttpPost, ActionName("AddPages")]
        public async Task< IActionResult> Upload(IFormFile file, int chapterId)
        {
            try
            {
                var chapter = await _AdminChapterRepository.GetById(chapterId);
                if (chapter == null)
            {
                return NotFound();
            }
                var folderName = $"{chapterId}{chapter.Title}";
                string folderId = _googleDriveAPIRepository.GetOrCreateFolder(folderName);

                _googleDriveAPIRepository.UploadFileToGoogleDrive(file, folderId);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при завантаженні файлу на Google Drive: {ex.Message}");
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ButtonClick(int chapterId)
        {
            var chapter = await _AdminChapterRepository.GetById(chapterId);
            string folderName = $"{chapterId}{chapter.Title}";

            string folderId = _googleDriveAPIRepository.GetOrCreateFolder(folderName);
            var modifiedPhotoLinks =  _googleDriveAPIRepository.GetModifiedFileLinks(folderId);
            await  _googleDriveAPIRepository.AddFileLinksToPagesWithChapters(modifiedPhotoLinks, chapterId);

            return RedirectToAction("Index");
        }



    }
}
