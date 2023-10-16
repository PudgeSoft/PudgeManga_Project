namespace PudgeManga_Project.Models
{
    public class Chapters
    {
        public int ChapterId { get; set; }
        public int MangaID { get; set; }
        public int ChapterNumber { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Url { get; set; }
    }
}
