using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels;
using PudgeManga_Project.ViewModels.MangaViewModels;

namespace PudgeManga_Project.Controllers
{
    public class MangaController : Controller
    {
        private readonly IMangaRepository<Manga, int> _mangaRepository;
        private readonly IChapterRepository<Chapter, int> _chapterRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        public MangaController(IMangaRepository<Manga, int> mangaRepository,
            IChapterRepository<Chapter, int> chapterRepository,
            IRatingRepository ratingRepository,
            IUserRepository userRepository,
            ICommentRepository commentRepository)
        {
            _mangaRepository = mangaRepository;
            _chapterRepository = chapterRepository;
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
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
            var comments = await _commentRepository.GetCommentsByMangaId(mangaId);

            var viewModel = new MangaChaptersViewModel
            {
                Manga = manga,
                Chapters = chapters,
                AverageRating = averageRating,
                Comments = comments
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Reading(int mangaId, int chapter)
        {
            var manga = await _mangaRepository.GetByIdReading(mangaId, chapter);
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

        [HttpPost]
        public async Task<IActionResult> RateManga(string userId, int mangaId, double value)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userRating = new Rating
                {
                    UserId = userId,
                    MangaId = mangaId,
                    Value = value
                };
                await _ratingRepository.AddRatingAsync(userRating);
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
