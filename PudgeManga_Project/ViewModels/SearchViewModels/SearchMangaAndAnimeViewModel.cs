using Microsoft.AspNetCore.Mvc.Rendering;
using PudgeManga_Project.Data.Enum;
using PudgeManga_Project.Models;
using PudgeManga_Project.ViewModels.AdminMangaViewModels;

namespace PudgeManga_Project.ViewModels.SearchViewModels
{
    public class SearchMangaAndAnimeViewModel
    {
        public List<int> MangaGenreIds { get; set; }
        public List<int> AnimeGenreIds { get; set; }
        public List<Anime> Animes {  get; set; }
        public List<Manga> Mangas { get; set; }
        public CreateMangaViewModel SearchMangaViewModel { get; set; }
        public CreateMangaViewModel SearchAnuimeViewModel { get; set; }
        public List<SelectListItem> AllGenresMangas { get; set; }
        public List<SelectListItem> AllGenresAnimes { get; set; }
        public SearchType SearchType { get; set; }
    }
}
