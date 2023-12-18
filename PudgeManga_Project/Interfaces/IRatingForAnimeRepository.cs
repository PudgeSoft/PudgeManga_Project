using PudgeManga_Project.Models;

namespace PudgeManga_Project.Interfaces
{
    public interface IRatingForAnimeRepository
    {
        Task<double> GetAnimeAverageRatingAsync(int animeId);
        Task AddRatingAsync(RatingForAnime rating);
        Task UpdateRatingAsync(RatingForAnime rating);
        Task<RatingForAnime> GetRatingAsync(int animeId, string userId);
    
    }
}
