using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Detail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Category(int id, int parentId)
        {
            return View();
        }
    }
}
