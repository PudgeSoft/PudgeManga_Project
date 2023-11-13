namespace PudgeManga_Project.ViewModels.AdminMangaViewModels
{
    public class EditMangaViewModel
    {
        public int Mangaid { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public int GenreId { get; set; }
    }
}
