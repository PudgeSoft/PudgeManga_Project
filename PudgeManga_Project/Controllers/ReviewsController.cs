using Microsoft.AspNetCore.Mvc;

namespace PudgeManga_Project.Controllers
{
    public class ReviewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
