using PudgeManga_Project.Models;

namespace PudgeManga_Project.ViewModels.MangaViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Manga> PopularManga { get; set; }
        public IEnumerable<Manga> LasUpdatedManga { get; set; }

    }
}
