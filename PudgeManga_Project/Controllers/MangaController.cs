using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Models;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels;

namespace PudgeManga_Project.Controllers
{
    public class MangaController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IRepository<Manga, int> _mangaRepository;
        public MangaController(IRepository<Manga, int> mangaRepository)
        {
            _mangaRepository = mangaRepository;
        }

        // GET: Mangas
        public async Task<IActionResult> Index()
        {
            var model = await _mangaRepository.GetAll();
            return View(model);
        }


        // GET: Mangas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var manga = await _mangaRepository.GetById(id);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }
        // GET: Mangas/MangaDetail/5
        [HttpGet]
        //[Route("MangaDetail/{id}")]
        public async Task<IActionResult> MangaDetails(int id)
        {
            var manga = await _mangaRepository.GetById(id);
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
                _mangaRepository.Add(manga);
                return RedirectToAction("Create");
            }
            return View(mangaViewModel);
        }

        // GET: Mangas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var manga = await _mangaRepository.GetById(id);
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
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", editMangaViewModel);
            }
            if (ModelState.IsValid) {
                var manga = new Manga
                {
                    MangaId = id,
                    Title = editMangaViewModel.Title,
                    Author = editMangaViewModel.Author,
                    Description = editMangaViewModel.Description,
                    CoverUrl = editMangaViewModel.CoverUrl,
                    GenreId = editMangaViewModel.GenreId,

                };
                _mangaRepository.UpdateAsync(manga);
            }
            
            return RedirectToAction("Index");
           
        }

        // GET: Mangas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var manga = await _mangaRepository.GetById(id);
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
            var manga = await _mangaRepository.GetById(id);
            if (manga == null)
            {
                return View("Delete error");
            }
            _mangaRepository.Delete(manga);
            _mangaRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
