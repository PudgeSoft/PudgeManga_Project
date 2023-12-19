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
        private readonly IRatingForMangaRepository _ratingForMangaRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        public MangaController(IMangaRepository<Manga, int> mangaRepository,
            IChapterRepository<Chapter, int> chapterRepository,
            IRatingForMangaRepository ratingForMangaRepository,
            IUserRepository userRepository,
            ICommentRepository commentRepository)
        {
            _mangaRepository = mangaRepository;
            _chapterRepository = chapterRepository;
            _ratingForMangaRepository = ratingForMangaRepository;
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
            var averageRating = await _ratingForMangaRepository.GetMangaAverageRatingAsync(mangaId);
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentViewModel model)
        {
            
            var comment = new Comment
            {
                CommentText = model.CommentText,
                CommentDate = DateTime.Now,
                ParentId = model.ParentId,
                
            };

            await _commentRepository.AddCommentAsync(comment);
            var manga = await _mangaRepository.GetById(model.MangaId);
            var mangaComment = new MangaComment
            {
                Manga = manga,
                Comment = comment
            };
            await _commentRepository.AddMangaCommentAsync(mangaComment);
            var updatedComments = await _commentRepository.GetCommentsForMangaAsync(model.MangaId);

            return PartialView("_CommentPartial", updatedComments);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetComments(int mangaId)
        {
            var comments = await _commentRepository.GetCommentsForMangaAsync(mangaId);

            return PartialView("_CommentPartial", comments);
        }

        [HttpPost]
        public async Task<IActionResult> RateManga(string userId, int mangaId, double value)
        {
            if (User.Identity.IsAuthenticated)
            {
                var existingRating = await _ratingForMangaRepository.GetRatingAsync(mangaId, userId);
                
                if (existingRating != null)
                {
                    existingRating.Value = value;
                    await _ratingForMangaRepository.UpdateRatingAsync(existingRating);
                }
                else
                {
                    var userRating = new RatingForManga
                    {
                        UserId = userId,
                        MangaId = mangaId,
                        Value = value
                    };

                    await _ratingForMangaRepository.AddRatingAsync(userRating);

                }
                    return Ok();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
