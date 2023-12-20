using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.ViewModels;

namespace PudgeManga_Project.Models.Repositories
{
    public class ChapterRepository : IChapterRepository<Chapter, int>
    {
        private readonly ApplicationDBContext _context;
        public ChapterRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Chapter>> GetChaptersForManga(int mangaId)
        {
            var chapters = await _context.Chapters
            .Where(c => c.MangaID == mangaId)
            .ToListAsync();

            return chapters;
        }
        public async Task<int> GetTotalChapters(int mangaId)
        {

            var totalChapters = await _context.Chapters
                .Where(c => c.MangaID == mangaId)
                .CountAsync();

            return totalChapters;
        }
        public async Task<List<Chapter>> GetAllAsync()
        {
            var allChapters = await _context.Chapters.ToListAsync();
            return allChapters;
        }

        public async Task<List<CalendarViewModel>> GetViewModelForCalendarAsync()
        {
            List<Chapter> chapters = await _context.Chapters.ToListAsync();

            Dictionary<DateTime, List<Chapter>> chaptersByDate = new Dictionary<DateTime, List<Chapter>>();

            foreach (var chapter in chapters)
            {
                DateTime publicationDate = chapter.PublicationDate.Date;

                if (!chaptersByDate.ContainsKey(publicationDate))
                {
                    chaptersByDate[publicationDate] = new List<Chapter>();
                }

                chaptersByDate[publicationDate].Add(chapter);
            }
            List<CalendarViewModel> calendarViewModelList = new List<CalendarViewModel>();

            for (int day = 1; day <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); day++)
            {
                DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, day);

                if (chaptersByDate.ContainsKey(currentDate))
                {
                    CalendarViewModel calendarViewModel = new CalendarViewModel
                    {
                        PublicationDate = currentDate,
                        Chapters = chaptersByDate[currentDate]
                    };

                    calendarViewModelList.Add(calendarViewModel);
                }
                else
                {

                    CalendarViewModel calendarViewModel = new CalendarViewModel
                    {
                        PublicationDate = currentDate,
                        Chapters = new List<Chapter>()
                        {
                            new Chapter()
                            {
                                Title = "Відсутні глави",
                                Url = "../src/img/no-photo.png"
                            }
                        }
                    };

                    calendarViewModelList.Add(calendarViewModel);
                }
            }

            return calendarViewModelList;
        }
    }
}
