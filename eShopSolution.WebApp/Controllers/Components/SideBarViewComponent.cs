using eShopSolution.ApiIntegration.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Controllers.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public SideBarViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryApiClient.GetAll(CultureInfo.CurrentCulture.Name);
            return View(categories); // Nếu khác Default.cshtml thì trả về View("abc", product) cho abc.cshtml
        }
    }
}