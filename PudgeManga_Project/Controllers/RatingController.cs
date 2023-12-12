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

        //[HttpPost]
        //public async Task<IActionResult> SetMangaRating(string userId, int mangaId, double value)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {

        //        var rating = await _dbContext.Ratings
        //            .Where(r => r.UserId == userId && r.AnimeId == mangaId)
        //            .FirstOrDefaultAsync();

        //        if (rating == null)
        //        {
        //            rating = new Rating
        //            {
        //                UserId = userId,
        //                MangaId = mangaId,
        //                Value = value
        //            };
        //            _dbContext.Ratings.Add(rating);
        //        }
        //        else
        //        {
        //            rating.Value = value;
        //        }

        //        await _dbContext.SaveChangesAsync();

        //        return Ok(rating.Value);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //}
    }
}
