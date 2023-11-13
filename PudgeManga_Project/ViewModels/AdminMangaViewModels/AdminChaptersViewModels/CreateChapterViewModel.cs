namespace PudgeManga_Project.ViewModels.AdminMangaViewModels.AdminChaptersViewModels
{
	public class CreateChapterViewModel
	{
        public int MangaId { get; set; }
        public int ChapterNumber { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Url { get; set; }
        
    }
}
