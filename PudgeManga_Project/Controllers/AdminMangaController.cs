﻿using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Data;
using PudgeManga_Project.Models;
using PudgeManga_Project.ViewModels;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models.Repositories;

namespace PudgeManga_Project.Controllers
{
    public class AdminMangaController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IAdminMangaRepository<Manga, int> _AdminMangaRepository;
        private readonly IChapterRepository<Chapter, int> _chapterRepository;

        public AdminMangaController(IAdminMangaRepository<Manga, int> adminMangaRepository, IChapterRepository<Chapter, int> chapterRepository) // Доданий параметр для IChapterRepository
        {
            _AdminMangaRepository = adminMangaRepository;
            _chapterRepository = chapterRepository;
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
        public async Task<IActionResult> Chapters(int id)
        {
            var chapters = await _chapterRepository.GetChaptersForManga(id);
            return View(chapters);
        }
        public IActionResult CreateChapter()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateChapters(CreateMangaViewModel mangaViewModel)
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
        public async Task<IActionResult> EditChapter(int id)
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
        public async Task<IActionResult> EditChapter(int id, EditMangaViewModel editMangaViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
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
        public async Task<IActionResult> DeleteChapter(int id)
        {
            var manga = await _AdminMangaRepository.GetById(id);
            if (manga == null)
            {
                return NotFound();
            }
            return View(manga);
        }

        // POST: Mangas/Delete/5
        [HttpPost, ActionName("DeleteChapter")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedChapter(int id)
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
    }
}
