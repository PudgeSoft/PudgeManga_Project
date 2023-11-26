using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PudgeManga_Project.ViewModels.AdminMangaViewModels
{
    public class EditMangaViewModel
    {
        public int MangaId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }

        [Display(Name = "Genres")]
        public List<int> GenreIds { get; set; }

        public List<SelectListItem> AllGenres { get; set; }
    }
}
