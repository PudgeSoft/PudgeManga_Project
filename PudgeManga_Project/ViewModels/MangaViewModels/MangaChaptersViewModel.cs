using PudgeManga_Project.Models;

namespace PudgeManga_Project.ViewModels.MangaViewModels
{
    public class MangaChaptersViewModel
    {
        public Manga Manga { get; set; }
        public IEnumerable<Chapter> Chapters { get; set; }
        public double AverageRating { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }

}
