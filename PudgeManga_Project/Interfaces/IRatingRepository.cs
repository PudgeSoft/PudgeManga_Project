using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Models;

namespace PudgeManga_Project.Interfaces
{
    public interface IRatingRepository
    {
        Task<double> GetAnimeAverageRatingAsync(int animeId);
        Task<double> GetMangaAverageRatingAsync(int mangaId);
        Task AddRatingAsync(Rating rating);
        
    }
}
