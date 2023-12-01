using Microsoft.AspNetCore.Mvc.Rendering;
using PudgeManga_Project.Models;
using PudgeManga_Project.ViewModels.AdminMangaViewModels;

namespace PudgeManga_Project.ViewModels
{
    public class SearchViewModel
    {
        public List<int> GenreIds { get; set; }
        public List<Manga> Mangas { get; set; }
        public CreateMangaViewModel SearchAndViewModel { get; set;}
        public List<SelectListItem> AllGenres { get; set; }
    }
}
