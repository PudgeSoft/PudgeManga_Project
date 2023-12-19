
﻿using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
﻿using Microsoft.AspNetCore.Authorization;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels.MangaViewModels;
using PudgeManga_Project.ViewModels.AnimeViewModels;
using PudgeManga_Project.ViewModels;

namespace PudgeManga_Project.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeRepository<Anime, int> _animeRepository;
        private readonly IAnimeSeasonsRepository<AnimeSeason, int> _seasonsRepository;
        private readonly IRatingForAnimeRepository _ratingForAnimeRepository;
        private readonly ICommentRepository _commentRepository;
        public AnimeController(IAnimeRepository<Anime, int> animeRepository,
            IAnimeSeasonsRepository<AnimeSeason, int> seasonsRepository,
            IRatingForAnimeRepository ratingForAnimeRepository,
            ICommentRepository commentRepository)
        {
            _animeRepository = animeRepository;
            _seasonsRepository = seasonsRepository;
            _ratingForAnimeRepository = ratingForAnimeRepository;
            _commentRepository = commentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _animeRepository.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> AnimeDetails(int animeId, int seasonId)
        {
            var anime = await _animeRepository.GetAnimeByIdAsync(animeId);

            if (anime == null)
            {
                return NotFound();
            }

            var seasons = await _animeRepository.GetSeasonsByAnimeIdAsync(animeId);

            if (seasons == null || !seasons.Any())
            {
                return NotFound();
            }

            var selectedSeason = seasons.FirstOrDefault(s => s.AnimeSeasonId == seasonId);

            if (selectedSeason == null)
            {
                // Якщо seasonId не знайдено серед сезонів аніме, ви можете повернутися до дефолтного сезону чи обробити по-іншому
                selectedSeason = seasons.First();
                // або return NotFound();
            }

            var episodes = await _animeRepository.GetEpisodesBySeasonIdAsync(selectedSeason.AnimeSeasonId);


            var averageRating = await _ratingForAnimeRepository.GetAnimeAverageRatingAsync(animeId);
            var viewModel = new AnimeDetailsViewModel
            {
                Anime = anime,
                Seasons = seasons,
                SelectedSeason = selectedSeason,
                Episodes = episodes,
                AverageRating = averageRating,
                Comments = new List<Comment>(),
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] AnimeSeasonCommentViewModel model)
        {
            var comment = new Comment
            {
                CommentText = model.CommentText,
                CommentDate = DateTime.Now,
                ParentId = model.ParentId,
            };

            await _commentRepository.AddCommentAsync(comment);
            var manga = await _seasonsRepository.GetSeasonById(model.AnimeSeasonId);
            var mangaComment = new AnimeSeasonComment
            {
                AnimeSeason = manga,
                Comment = comment
            };
            await _commentRepository.AddAnimeSeasonCommentAsync(mangaComment);
            var updatedComments = await _commentRepository.GetCommentsForMangaAsync(model.AnimeSeasonId);

            return PartialView("_CommentPartial", updatedComments);
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int seasonId)
        {
            var comments = await _commentRepository.GetCommentsForAnimeSeasonAsync(seasonId);

            return PartialView("_CommentPartial", comments);
        }

        [HttpPost]
        public async Task<IActionResult> RateAnime(string userId, int animeId, double value)
        {
            if (User.Identity.IsAuthenticated)
            {
                var existingRating = await _ratingForAnimeRepository.GetRatingAsync(animeId, userId);

                if (existingRating != null)
                {
                    existingRating.Value = value;
                    await _ratingForAnimeRepository.UpdateRatingAsync(existingRating);
                }
                else
                {
                    var userRating = new RatingForAnime
                    {
                        UserId = userId,
                        AnimeId = animeId,
                        Value = value
                    };

                    await _ratingForAnimeRepository.AddRatingAsync(userRating);

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
