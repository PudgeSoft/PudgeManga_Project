using Microsoft.AspNetCore.Mvc;

namespace PudgeManga_Project.Interfaces
{
    public interface IRatingRepository
    {
        Task<double> GetAnimeAverageRatingAsync(int animeId);
        Task<double> GetMangaAverageRatingAsync(int mangaId);
        //Task<int> GetAnimeRating(int userId, int animeId);
        //Task<int> GetMangaRating(int userId, int mangaId);

    }
}
