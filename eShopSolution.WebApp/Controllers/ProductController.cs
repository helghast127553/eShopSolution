using eShopSolution.ApiIntegration.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        public ProductController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            return View(await _productApiClient.GetProductDetail(id));
        }

        [HttpGet]
        public IActionResult Category(int id, int parentId)
        {
            return View();
        }
    }
}
