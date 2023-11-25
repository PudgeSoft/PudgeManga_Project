using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Data;
using PudgeManga_Project.Models;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels.AdminMangaViewModels;
using PudgeManga_Project.ViewModels.AdminMangaViewModels.AdminChaptersViewModels;
using PudgeManga_Project.Helpers;

namespace PudgeManga_Project.Controllers
{
    public class AdminMangaController : Controller
    {
        private readonly IAdminMangaRepository<Manga, int> _AdminMangaRepository;
        private readonly IAdminChapterRepository<Chapter, int> _AdminChapterRepository;
        private readonly IGoogleDriveAPIRepository<IFormFile> _googleDriveAPIRepository;
        public AdminMangaController(IAdminMangaRepository<Manga, int> adminMangaRepository, 
            IAdminChapterRepository<Chapter, int> adminChapterRepository,
            IChapterRepository<Chapter, int> chapterRepository,
            IGoogleDriveAPIRepository<IFormFile> googleDriveAPIRepository) 
        {
            _AdminMangaRepository = adminMangaRepository;
            _AdminChapterRepository = adminChapterRepository;
            _googleDriveAPIRepository = googleDriveAPIRepository;

        }

        // GET: Mangas
        public async Task<IActionResult> Index()
        {
            var model = await _AdminMangaRepository.GetAll();
            return View(model);
        }


        // GET: Mangas/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mangas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMangaViewModel mangaViewModel)
        {
            if (ModelState.IsValid)
            {
                var manga = new Manga
                {
                    Title = mangaViewModel.Title,
                    Author = mangaViewModel.Author,
                    Description = mangaViewModel.Description,
                    CoverUrl = mangaViewModel.CoverUrl,
                    GenreId = mangaViewModel.GenreId,
                };
               await _AdminMangaRepository.Add(manga);
                return RedirectToAction("Create");
            }
            return View(mangaViewModel);
        }

        // GET: Mangas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var manga = await _AdminMangaRepository.GetById(id);
            if (manga == null)
            {
                return NotFound();
            }
            return View(manga);
        }

        // POST: Mangas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditMangaViewModel editMangaViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Manga");
                return View("Edit", editMangaViewModel);
            }
            if (ModelState.IsValid)
            {
                var manga = new Manga
                {
                    MangaId = id,
                    Title = editMangaViewModel.Title,
                    Author = editMangaViewModel.Author,
                    Description = editMangaViewModel.Description,
                    CoverUrl = editMangaViewModel.CoverUrl,
                    GenreId = editMangaViewModel.GenreId,

                };
               await _AdminMangaRepository.UpdateAsync(manga);
            }

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
                ModelState.AddModelError("", "Failed to edit chapter");
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
        public async Task<IActionResult> Upload(IFormFile file, int chapterId)
        {
            try
            {
                var folderName = $"{chapterId}";
                var folderId = _googleDriveAPIRepository.UploadFileToGoogleDrive(file, folderName);

                var modifiedPhotoLinks = await _googleDriveAPIRepository.GetModifiedFileLinks(folderId);
                await _googleDriveAPIRepository.AddFileLinksToPagesWithChapters(modifiedPhotoLinks, chapterId);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error uploading file to Google Drive: {ex.Message}");
                return RedirectToAction("Index"); 
            }
        }


    }
}
