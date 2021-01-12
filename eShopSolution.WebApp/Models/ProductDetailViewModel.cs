using eShopSolution.ViewModels.Catalog.Category;
using eShopSolution.ViewModels.Catalog.Products;

namespace eShopSolution.WebApp.Models
{
    public class ProductDetailViewModel
    {
        public ProductViewModel product { get; set; }
        public CategoryViewModel category { get; set; }
    }
}