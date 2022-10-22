using eShopSolution.ApiIntegration.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebApp.Controllers.Components
{

    public class NavbarHeaderViewComponent: ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public NavbarHeaderViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _categoryApiClient.GetAll());
        }
    }
}
