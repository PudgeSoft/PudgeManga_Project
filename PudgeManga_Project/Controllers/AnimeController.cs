using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;

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
    }
}
