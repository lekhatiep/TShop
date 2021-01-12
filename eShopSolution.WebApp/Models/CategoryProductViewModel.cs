using eShopSolution.ViewModels.Catalog.Category;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;

namespace eShopSolution.WebApp.Models
{
    public class CategoryProductViewModel
    {
        public PagedResult<ProductViewModel> products { get; set; }
        public CategoryViewModel category { get; set; }
    }
}