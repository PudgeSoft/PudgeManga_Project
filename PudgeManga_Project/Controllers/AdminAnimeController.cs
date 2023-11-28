using Microsoft.AspNetCore.Mvc;

namespace PudgeManga_Project.Controllers
{
    public class AdminAnimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
