using PudgeManga_Project.Models;
using PudgeManga_Project.ViewModels;
namespace PudgeManga_Project.Interfaces
{
    public interface ICommentRepository
    {
        Task<IQueryable<Comment>> GetAllAsync();
        Task AddCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> GetCommentsByMangaId(int mangaId);
    }

}
