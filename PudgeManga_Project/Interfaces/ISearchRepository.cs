using PudgeManga_Project.Models;

namespace PudgeManga_Project.Interfaces
{
    public interface ISearchRepository
    {
        Task<IEnumerable<Manga>> SearchMangaAsync(string SearchString, List<int> genres);
    }
}
