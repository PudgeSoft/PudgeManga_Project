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
            var allChapters = await _chapterRepository.GetAllAsync();
            var date = DateTime.Now;
            Console.WriteLine(date.Day);

            Console.WriteLine(DateTime.DaysInMonth(date.Year, date.Month));
            return View(allChapters);
        }
        public List<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                             .Select(day => new DateTime(year, month, day))
                             .ToList();
        }
    }
}
