using System.ComponentModel.DataAnnotations;

namespace PudgeManga_Project.ViewModels.AdminMangaViewModels.AdminChaptersViewModels
{
    public class EditChapterViewModel
    {
        public int ChapterId { get; set; }
        public int ChapterNumber { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Url { get; set; }

    }
}
