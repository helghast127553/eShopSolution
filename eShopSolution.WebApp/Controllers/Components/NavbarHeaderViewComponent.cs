using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebApp.Controllers.Components
{
    public class NavbarHeaderViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
