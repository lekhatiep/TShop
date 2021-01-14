using eShopSolution.ApiIntegration.Interface;
using eShopSolution.Utilities.Constant;
using eShopSolution.ViewModels.Catalog.Category;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IRoleApiClient _roleApiClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient,
            IRoleApiClient roleApiClient,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            ICategoryApiClient categoryApiClient
            )
        {
            _productApiClient = productApiClient;
            _roleApiClient = roleApiClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 5)
        {
            var currentLang = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.DefaultLanguageId);
            var request = new GetManageProductPagingRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Keyword = keyword,
                LanguageId = currentLang,
                CategoryId = categoryId
            };
            ViewBag.Keyword = keyword;
            var data = await _productApiClient.GetProductPaging(request);
            var categories = await _categoryApiClient.GetAll(currentLang);
            ViewBag.Categories = categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = categoryId.HasValue && categoryId.Value == x.Id
            }); ;

            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }

            return View(data.ResultObject);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest productCreateRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(productCreateRequest);
            }
            var result = await _productApiClient.CreateProduct(productCreateRequest);
            if (result)
            {
                TempData["success"] = "Thêm sản phẩm thành công";
                return RedirectToAction("Index", "Product");
            }
            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CategoryAssign(int Id)
        {
            var categoryAssignRequest = await GetCategoryAssignRequest(Id);

            return View(categoryAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> AssignCategory(CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _productApiClient.AssginCategory(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["success"] = "Gán danh mục thành thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var categoryAssignRequest = await GetCategoryAssignRequest(request.Id);
            return View(categoryAssignRequest);
        }

        private async Task<CategoryAssignRequest> GetCategoryAssignRequest(int productId)
        {
            var currentLang = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.DefaultLanguageId);
            var product = await _productApiClient.GetById(productId, currentLang);
            var categoryList = await _categoryApiClient.GetAll(currentLang);
            var categoryAssignRequest = new CategoryAssignRequest();

            if (categoryList != null || product != null)
            {
                foreach (var category in categoryList)
                {
                    categoryAssignRequest.Categories.Add(new SelectedItem()
                    {
                        Id = category.Id.ToString(),
                        Name = category.Name,
                        Selected = product.Categories.Contains(category.Name)
                    });
                }
            }
            return categoryAssignRequest;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.DefaultLanguageId);
            var product = await _productApiClient.GetById(id, languageId);
            var productUpdateView = new ProductUpdateRequest()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Details = product.Details,
                SeoAlias = product.SeoAlias,
                SeoDescription = product.SeoDescription,
                SeoTitle = product.SeoTitle
            };

            return View(productUpdateView);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _productApiClient.UpdateProduct(request);
            if (result)
            {
                TempData["success"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.DefaultLanguageId);
            var product = await _productApiClient.GetById(id, languageId);

            return View(new ProductDeleteRequest
            {
                Id = product.Id,
                Name = product.Name,
                Image = product.Image
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] ProductDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _productApiClient.DeleteProduct(request.Id);
            if (result)
            {
                TempData["success"] = "Xóa sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Xóa sản phẩm thất bại");
            return View();
        }
    }
}