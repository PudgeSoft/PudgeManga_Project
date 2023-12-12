namespace PudgeManga_Project.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public int? ParentId { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
    }
}
