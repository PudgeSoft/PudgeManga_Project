namespace PudgeManga_Project.Models
{
    public class Manga
    {
        public int MangaId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public int GenreId { get; set; }


    }
}
