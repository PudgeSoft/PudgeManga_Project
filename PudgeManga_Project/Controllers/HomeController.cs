using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Data;
using PudgeManga_Project.Models;
using PudgeManga_Project.Models.Repositories;
using System.Diagnostics;

namespace PudgeManga_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IRepository<Manga, int> _mangaRepository;
        public HomeController(IRepository<Manga, int> mangaRepository)
        {
            _mangaRepository = mangaRepository;
        }

        // GET: Mangas
        public async Task<IActionResult> Index()
        {
            var model = await _mangaRepository.GetAll();
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