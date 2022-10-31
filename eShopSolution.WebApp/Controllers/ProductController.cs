using eShopSolution.ApiIntegration;
using eShopSolution.ApiIntegration.Abstraction;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
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
        public async Task<IActionResult> Category(int subCategoryId, int parentCategoryId , string categoryName, int pageIndex = 0, int pageSize = 15)
        {
            ++pageIndex;
            PagedResult<ProductViewModel> data = null;
            ViewBag.categories = await _categoryApiClient.GetAll();
            ViewBag.categoryName = categoryName;

            if (subCategoryId != 0)
            {
                data = await _productApiClient.GetAllProductsByCategory(subCategoryId, 0, pageIndex, pageSize);
                ViewBag.subCategoryId = subCategoryId;
                ViewBag.parentCategoryId = null;
            }
            else
            {
                data = await _productApiClient.GetAllProductsByCategory(0, parentCategoryId, pageIndex, pageSize);
                ViewBag.parentCategoryId = parentCategoryId;
                ViewBag.subCategoryId = null;
            }

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsQuantity(int subCategoryId, int parentCategoryId, int pageSize = 15)
        {
            PagedResult<ProductViewModel> data = null;
            ViewBag.categories = await _categoryApiClient.GetAll();

            if (subCategoryId != 0)
            {
                data = await _productApiClient.GetAllProductsByCategory(subCategoryId, 0, 1, pageSize);
                ViewBag.subCategoryId = subCategoryId;
                ViewBag.parentCategoryId = null;
            }
            else
            {
                data = await _productApiClient.GetAllProductsByCategory(0, parentCategoryId, 1, pageSize);
                ViewBag.parentCategoryId = parentCategoryId;
                ViewBag.subCategoryId = null;
            }

            return View("Category", data);
        }
    }
}
