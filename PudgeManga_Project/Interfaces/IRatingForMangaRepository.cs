using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Models;

namespace PudgeManga_Project.Interfaces
{
    public interface IRatingForMangaRepository
    {
        Task<double> GetMangaAverageRatingAsync(int mangaId);
        Task AddRatingAsync(RatingForManga rating);
        Task UpdateRatingAsync(RatingForManga rating);
        Task<RatingForManga> GetRatingAsync(int mangaId, string userId);
    }
}
