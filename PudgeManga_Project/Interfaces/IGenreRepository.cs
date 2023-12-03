using PudgeManga_Project.Models;

namespace PudgeManga_Project.Interfaces
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllGenresAsync();
        Task AddGenreAsync(Genre genre);
    }
}
