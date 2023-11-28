using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;

namespace PudgeManga_Project.Controllers
{
    public class AdminAnimeController : Controller
    {
        private readonly IAdminAnimeRepository<Anime, int> _adminAnimeRepository;
        private readonly IGoogleDriveAPIRepository<IFormFile> _googleDriveAPIRepository;
        private readonly IGenreRepository _genreRepository;
        public AdminAnimeController(IGoogleDriveAPIRepository<IFormFile> googleDriveAPIRepository,
            IGenreRepository genreRepository)
        {
            
            _googleDriveAPIRepository = googleDriveAPIRepository;
            _genreRepository = genreRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _adminAnimeRepository.GetAll();
            return View(model);
        }
    }
}
