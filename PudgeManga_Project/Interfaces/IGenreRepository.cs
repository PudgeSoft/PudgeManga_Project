using PudgeManga_Project.Models;

namespace PudgeManga_Project.Interfaces
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllGenres();
    }
}
