using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebApp.Controllers.Components
{
    public class SliderViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
