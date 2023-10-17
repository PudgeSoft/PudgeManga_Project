namespace PudgeManga_Project.Models
{
    public class Popularity
    {
        public int Id { get; set; }
        public int MangaId { get; set; }
        public int ViewsCount { get; set; }
        public int CommentsCount { get; set; }
        public decimal AverageRating { get; set; }
    }
}
