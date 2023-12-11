using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Models;

namespace PudgeManga_Project.Controllers
{
    public class RatingController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public RatingController(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpPost]
        public async Task<IActionResult> SetRating(string userId, int animeId, int value)
        {
            var rating = await _dbContext.Ratings
                .Where(r => r.UserId.Equals(userId) && r.AnimeId == animeId)
                .FirstOrDefaultAsync();

            if (rating == null)
            {
                rating = new Rating
                {
                    UserId = userId,
                    AnimeId = animeId,
                    Value = value
                };
                _dbContext.Ratings.Add(rating);
            }
            else
            {
                rating.Value = value;
            }

            await _dbContext.SaveChangesAsync();

            return Ok(rating.Value);
        }
    }
}
