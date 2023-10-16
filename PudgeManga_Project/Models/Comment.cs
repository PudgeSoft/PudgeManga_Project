namespace PudgeManga_Project.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int MangaId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
