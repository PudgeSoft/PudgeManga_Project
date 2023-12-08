using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels.MangaViewModels;

namespace PudgeManga_Project.Controllers
{
    public class MangaController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMangaRepository<Manga, int> _mangaRepository;
        private readonly IChapterRepository<Chapter,int> _chapterRepository;
        private readonly IRatingRepository _ratingRepository;

        public MangaController(IMangaRepository<Manga, int> mangaRepository,
            IChapterRepository<Chapter,int> chapterRepository, 
            IRatingRepository ratingRepository) 
        {
            _mangaRepository = mangaRepository;
            _chapterRepository = chapterRepository;
            _ratingRepository = ratingRepository;
        }

        // GET: Mangas
        public async Task<IActionResult> Index()
        {
            var model = await _mangaRepository.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> MangaDetails(int mangaId)
        {
            var manga = await _mangaRepository.GetById(mangaId);
            if (manga == null)
            {
                return NotFound();
            }
            var chapters = await _chapterRepository.GetChaptersForManga(mangaId);
            var averageRating = await _ratingRepository.GetMangaAverageRatingAsync(mangaId);
            var viewModel = new MangaChaptersViewModel
            {
                Manga = manga,
                Chapters = chapters,
                AverageRating = averageRating
            };
            return View(viewModel);
        }
        public async Task<IActionResult> Reading(int mangaId,int chapter)
        {
            var manga = await _mangaRepository.GetByIdReading(mangaId,chapter);
            if (manga == null)
            {
                return NotFound();
            }
            var totalChapters = await _chapterRepository.GetTotalChapters(mangaId);
            var viewModel = new MangaReadingViewModel
            {
                Manga = manga,
                ChapterNumber = chapter,
                TotalChapters = totalChapters
            };

            return View(viewModel);
        }
        public async Task<IActionResult> Comments(int mangaId)
        {
            var manga = await _mangaRepository.GetById(mangaId);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }

    }
}
