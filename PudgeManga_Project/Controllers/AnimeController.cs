
﻿using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
﻿using Microsoft.AspNetCore.Authorization;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels.MangaViewModels;
using PudgeManga_Project.ViewModels.AnimeViewModels;

namespace PudgeManga_Project.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeRepository<Anime, int> _animeRepository;
        private readonly IAnimeSeasonsRepository<AnimeSeason, int> _seasonsRepository;
        public AnimeController(IAnimeRepository<Anime, int> animeRepository,
            IAnimeSeasonsRepository<AnimeSeason, int> seasonsRepository)
        {
            _animeRepository = animeRepository;
            _seasonsRepository = seasonsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _animeRepository.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> AnimeDetails(int animeId)
        {
            var anime = await _animeRepository.GetByIdAsync(animeId);
            if (anime == null)
            {
                return NotFound();
            }
            var seasons = await _seasonsRepository.GetSeasonsForAnimeAsync(animeId);
            var viewModel = new AnimeDetailsViewModel
            {
                Anime = anime,
                AnimeSeasons = seasons
            };
            return View(viewModel);
        }
    }
}
