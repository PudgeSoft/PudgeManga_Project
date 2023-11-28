using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PudgeManga_Project.ViewModels.AdminMangaViewModels
{
    public class EditMangaViewModel
    {
        public int MangaId { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Author field is required.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The CoverUrl field is required.")]
        public string CoverUrl { get; set; }

        [Required(ErrorMessage = "The Type field is required.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "The Publish field is required.")]
        public string Publish { get; set; }
        [Required(ErrorMessage = "The Artist field is required.")]
        public string Artist { get; set; }
        [Required(ErrorMessage = "The Translator field is required.")]
        public string Translator { get; set; }
        [Display(Name = "Genres")]
        [Required(ErrorMessage = "The GenreId field is required.")]
        public List<int> GenreIds { get; set; }

        public List<SelectListItem> AllGenres { get; set; }
    }
}
