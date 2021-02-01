using TShopSolution.ViewModels.Catalog.Category;
using TShopSolution.ViewModels.Catalog.Products;
using TShopSolution.ViewModels.Common;

namespace TShopSolution.WebApp.Models
{
    public class CategoryProductViewModel
    {
        public PagedResult<ProductViewModel> products { get; set; }
        public CategoryViewModel category { get; set; }
    }
}