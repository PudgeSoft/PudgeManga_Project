using PudgeManga_Project.Models;

namespace PudgeManga_Project.Interfaces
{
    public interface IMangaRepository<T1, T2> where T1 : class
    {
        Task<IEnumerable<T1>> GetAllAsync();
        Task<T1> GetById(T2 id);
        Task<T1> GetByIdReading(T2 id , int chapterNumber);
        Task<IEnumerable<Manga>> GetPopularMangaAsync(int count);
        Task<IEnumerable<Manga>> GetRecentlyUpdatedMangaAsync(int count);
    }
}
