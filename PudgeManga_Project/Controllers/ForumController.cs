using Microsoft.AspNetCore.Mvc;

namespace PudgeManga_Project.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
