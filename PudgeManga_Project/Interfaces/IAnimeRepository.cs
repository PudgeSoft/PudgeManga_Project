using PudgeManga_Project.Models;

namespace PudgeManga_Project.Interfaces
{
    public interface IAnimeRepository<T1, T2> where T1 : class
    {
        Task<IEnumerable<T1>> GetAllAsync();
        Task<Anime> GetAnimeByIdAsync(int animeId);
        Task<List<AnimeSeason>> GetSeasonsByAnimeIdAsync(int animeId);
        Task<List<AnimeEpisode>> GetEpisodesBySeasonIdAsync(int seasonId);
    }
}
