using PudgeManga_Project.Models;
using PudgeManga_Project.ViewModels;
namespace PudgeManga_Project.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> AddCommentAsync(Comment comment);
        Task AddMangaCommentAsync(MangaComment mangaComment);
        Task AddAnimeSeasonCommentAsync(AnimeSeasonComment animeSeasonComment);
        Task<IEnumerable<Comment>> GetCommentsForAnimeSeasonAsync(int seasonId);
        Task<List<Comment>> GetAllAsync();
        Task<IEnumerable<Comment>> GetCommentsByMangaId(int mangaId);
        Task<IEnumerable<Comment>> GetCommentsForMangaAsync(int mangaId);
    }

}
