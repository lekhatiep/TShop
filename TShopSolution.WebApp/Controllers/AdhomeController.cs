using TShopSolution.Utilities.Constant;
using TShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace TShopSolution.WebApp.Controllers
{
    public class AdhomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AdhomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = User.Identity.Name;
            return View();
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

        [HttpPost]
        public IActionResult Language(NavigationViewModel navigationViewModel)
        {
            HttpContext.Session.SetString(SystemConstant.AppSettings.DefaultLanguageId, navigationViewModel.CurrentLanguageId);

            return Redirect(navigationViewModel.ReturnURL);
        }
    }
}