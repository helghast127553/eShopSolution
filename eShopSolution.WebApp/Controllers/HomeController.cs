using eShopSolution.ApiIntegration.Abstraction;
using eShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eShopSolution.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductApiClient _productApiClient;

        public HomeController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _productApiClient.GetAll());
        }
    }
}