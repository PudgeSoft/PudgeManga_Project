using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
using PudgeManga_Project.ViewModels.MangaViewModels;
using System.Diagnostics;

namespace PudgeManga_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMangaRepository<Manga, int> _mangaRepository;
        public HomeController(IMangaRepository<Manga, int> mangaRepository)
        {
            _mangaRepository = mangaRepository;
        }


        public async Task<IActionResult> Index()
        {
            var popularManga = await _mangaRepository.GetPopularMangaAsync(5);
            var recentlyUpdatedManga = await _mangaRepository.GetRecentlyUpdatedMangaAsync(5);

            var model = new HomeViewModel
            {
                PopularManga = popularManga,
                LasUpdatedManga = recentlyUpdatedManga
            };

            return View(model);
        }



        public IActionResult TermsOfUse()
        {
            return View();
        }

        public IActionResult Advertisement()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}