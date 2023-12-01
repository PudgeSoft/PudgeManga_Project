using PudgeManga_Project.Models;

namespace PudgeManga_Project.Interfaces
{
    public interface IAnimeGenreRepository
    {
        Task<List<GenreForAnime>> GetAllGenresAsync();
        Task AddGenreAsync(GenreForAnime genre);
    }
}
