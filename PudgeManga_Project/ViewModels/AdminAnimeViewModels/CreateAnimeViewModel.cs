using Microsoft.AspNetCore.Mvc.Rendering;

namespace PudgeManga_Project.ViewModels.AdminAnimeViewModels
{
    public class CreateAnimeViewModel
    {
        public int AnimeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Director { get; set; }
        public string Studio { get; set; }
        public string Dubbing { get; set; }
        public string Type { get; set; }
        public int ReleaseYear { get; set; }
        public List<int> AnimeGenreIds { get; set; }

        public List<SelectListItem> AllGenres { get; set; }
    }
}
