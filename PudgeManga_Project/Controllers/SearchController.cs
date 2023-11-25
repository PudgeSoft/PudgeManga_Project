using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PudgeManga_Project.Data;

namespace PudgeManga_Project.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDBContext _context;
        public SearchController(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string SearchString)
        {
            ViewData["CurrentFilter"] = SearchString;
            var manga = from m in _context.Mangas
                        select m;
            if(!String.IsNullOrEmpty(SearchString) )
            {
                manga = manga.Where(m => m.Title.Contains(SearchString));
            }

            return View(manga);
        }
    }
}
