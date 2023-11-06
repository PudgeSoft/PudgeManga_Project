using PudgeManga_Project.Models;

namespace PudgeManga_Project.ViewModels
{
    public class MangaChaptersViewModel
    {
        public Manga Manga { get; set; }
        public IEnumerable<Chapter> Chapters { get; set; }
    }
}
