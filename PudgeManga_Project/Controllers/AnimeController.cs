
﻿using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
﻿using Microsoft.AspNetCore.Authorization;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels.MangaViewModels;

namespace PudgeManga_Project.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeRepository<Anime, int> _animeRepository;

        public AnimeController(IAnimeRepository<Anime, int> animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _animeRepository.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> AnimeDetails(int animeId)
        {
            var anime = await _mangaRepository.GetById(animeId);
            if (anime == null)
            {
                return NotFound();
            }
            var episodes = await _seasonRepository.GetChaptersForManga(animeId);
            var viewModel = new MangaChaptersViewModel
            {
                Manga = anime,
                Chapters = chapters
            };
            return View(viewModel);
        }
    }
}
