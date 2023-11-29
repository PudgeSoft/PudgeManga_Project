using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PudgeManga_Project.Controllers
{
    public class AnimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
