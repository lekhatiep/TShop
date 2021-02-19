using TShopSolution.ViewModels.Catalog.Category;
using TShopSolution.ViewModels.Catalog.Products;

namespace TShopSolution.WebApp.Models
{
    public class ProductDetailViewModel
    {
        public ProductViewModel product { get; set; }
        public CategoryViewModel category { get; set; }
    }
}