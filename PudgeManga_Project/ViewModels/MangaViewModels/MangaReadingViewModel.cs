using PudgeManga_Project.Models;

namespace PudgeManga_Project.ViewModels.MangaViewModels
{
    public class MangaReadingViewModel
    {
        public Manga Manga { get; set; }
        public int ChapterNumber { get; set; }
        public int TotalChapters { get; set; }
    }
}
