
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

            var viewModel = new AnimeDetailsViewModel
            {
                Anime = anime,
                Seasons = seasons,
                SelectedSeason = selectedSeason,
                Episodes = episodes
            };

            return View(viewModel);
        }

    }
}
