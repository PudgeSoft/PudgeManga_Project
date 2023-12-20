namespace PudgeManga_Project.ViewModels
{
    public class CommentViewModel
    {
        public int MangaId { get; set; }
        public string CommentText { get; set; }
        public int? ParentId { get; set; }
    }

}
