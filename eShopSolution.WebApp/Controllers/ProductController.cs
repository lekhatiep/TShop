using eShopSolution.ApiIntegration.Interface;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IProductApiClient _productApiClient;

        public ProductController(ICategoryApiClient categoryApiClient, IProductApiClient productApiClient)
        {
            _categoryApiClient = categoryApiClient;
            _productApiClient = productApiClient;
        }

        public IActionResult Index()
        {
            return Redirect("Index");
        }

        public async Task<IActionResult> Detail(int id, string culture)
        {
            var product = await _productApiClient.GetById(id, culture);
            var viewDetail = new ProductDetailViewModel
            {
                product = product
            };
            return View(viewDetail);
        }

        public async Task<IActionResult> Category(int id, string culture, int page = 1)
        {
            var product = await _productApiClient.GetProductPaging(new GetManageProductPagingRequest
            {
                CategoryId = id,
                LanguageId = culture,
                PageIndex = page,
                PageSize = 10
            });

            return View(new CategoryProductViewModel
            {
                products = product.ResultObject,
                category = await _categoryApiClient.GetCategoryById(id, culture)
            });
        }
    }
}