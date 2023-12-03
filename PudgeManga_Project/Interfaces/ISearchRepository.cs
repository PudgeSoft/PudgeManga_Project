using PudgeManga_Project.Data.Enum;
using PudgeManga_Project.Models;

namespace PudgeManga_Project.Interfaces
{
    public interface ISearchRepository
    {
        Task<IEnumerable<Manga>> SearchMangaAsync(string searchString, List<int> genres, SearchType searchType);
        Task<IEnumerable<Anime>> SearchAnimeAsync(string searchString, List<int> genres, SearchType searchType);
    }
}
