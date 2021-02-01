using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using LazZiya.ExpressLocalization;
using TShopSolution.ApiIntegration.Interface;
using System.Globalization;
using TShopSolution.Utilities.Constant;

namespace TShopSolution.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISharedCultureLocalizer _loc;
        private readonly ISlideApiClient _slideApiClient;
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public HomeController(
            ILogger<HomeController> logger,
            ISharedCultureLocalizer loc,
            ISlideApiClient slideApiClient,
            IProductApiClient productApiClient,
            ICategoryApiClient categoryApiClient
            )
        {
            _logger = logger;
            _loc = loc;
            _slideApiClient = slideApiClient;
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentCulture.Name;
            var msg = _loc.GetLocalizedString("Vietnamese");
            var slides = await _slideApiClient.GetAll();
            var featuredProducts = await _productApiClient.GetFeaturedProduct(culture, SystemConstant.ProductSettings.NumberOfTakeFeatured);
            var latestProducts = await _productApiClient.GetLatestProduct(culture, SystemConstant.ProductSettings.NumberOfLatest);

            var viewModel = new HomeViewModel()
            {
                slides = slides,
                featuredProducts = featuredProducts,
                latestProducts = latestProducts
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}