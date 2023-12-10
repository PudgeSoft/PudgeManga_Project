using PudgeManga_Project.Models;
using PudgeManga_Project.ViewModels;
namespace PudgeManga_Project.Interfaces
{
    public interface ICommentRepository
    {
        IQueryable<Comment> GetAll();
        CommentViewModel AddComment(CommentViewModel comment);
    }

}
