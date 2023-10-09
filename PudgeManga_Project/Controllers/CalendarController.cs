using Microsoft.AspNetCore.Mvc;

namespace PudgeManga_Project.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
