using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;

namespace PudgeManga_Project.Controllers
{
    public class CalendarController : Controller
    {
        private readonly IMangaRepository<Manga, int> _mangaRepository;
        private readonly IChapterRepository<Chapter, int> _chapterRepository;

        public CalendarController(IMangaRepository<Manga, int> mangaRepository,
            IChapterRepository<Chapter, int> chapterRepository)
        {
            _mangaRepository = mangaRepository;
            _chapterRepository = chapterRepository;
        }
        public async Task<IActionResult> Index()
        {
            var calendarViewModel = await _chapterRepository.GetViewModelForCalendarAsync();
            return View(calendarViewModel);
        }
       
    }
}
