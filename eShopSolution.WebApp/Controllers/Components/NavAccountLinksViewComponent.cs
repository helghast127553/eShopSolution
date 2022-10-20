using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebApp.Controllers.Components
{
    public class NavAccountLinksViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string link)
        {
            ViewBag.link = link;
            return View();
        }
    }
}
