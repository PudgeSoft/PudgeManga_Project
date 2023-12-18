namespace PudgeManga_Project.ViewModels
{
    public class CommentViewModel
    {
        public int MangaId { get; set; }
        public string CommentText { get; set; }
        public int? ParentId { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
    }
}
