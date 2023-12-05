using PudgeManga_Project.Models;
using PudgeManga_Project.ViewModels;

namespace PudgeManga_Project.Interfaces
{
    public interface IChapterRepository<T1, T2> where T1 : class
    {
        Task<IEnumerable<T1>> GetChaptersForManga(int T2);
        Task<int> GetTotalChapters(int mangaId);
        Task<List<T1>> GetAllAsync();
        Task<List<CalendarViewModel>> GetViewModelForCalendarAsync();
    }
}
