using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace eShopSolution.WebApp.Controllers.Components
{
    public class SideBarAccountViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
