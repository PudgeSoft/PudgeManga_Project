using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Models;

namespace PudgeManga_Project.Controllers
{
    public class MangaController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MangaController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Manga
        public async Task<IActionResult> Index()
        {
              return _context.Mangas != null ? 
                          View(await _context.Mangas.ToListAsync()) :
                          Problem("Entity set 'ApplicationDBContext.Mangas'  is null.");
        }

        // GET: Manga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mangas == null)
            {
                return NotFound();
            }

            var manga = await _context.Mangas
                .FirstOrDefaultAsync(m => m.MangaId == id);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }

        // GET: Manga/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MangaId,Title,Author,Description,CoverUrl,GenreId")] Manga manga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manga);
        }

        // GET: Manga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mangas == null)
            {
                return NotFound();
            }

            var manga = await _context.Mangas.FindAsync(id);
            if (manga == null)
            {
                return NotFound();
            }
            return View(manga);
        }

        // POST: Manga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MangaId,Title,Author,Description,CoverUrl,GenreId")] Manga manga)
        {
            if (id != manga.MangaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MangaExists(manga.MangaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(manga);
        }

        // GET: Manga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mangas == null)
            {
                return NotFound();
            }

            var manga = await _context.Mangas
                .FirstOrDefaultAsync(m => m.MangaId == id);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }

        // POST: Manga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mangas == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Mangas'  is null.");
            }
            var manga = await _context.Mangas.FindAsync(id);
            if (manga != null)
            {
                _context.Mangas.Remove(manga);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MangaExists(int id)
        {
          return (_context.Mangas?.Any(e => e.MangaId == id)).GetValueOrDefault();
        }
    }
}
