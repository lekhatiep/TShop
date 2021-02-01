using TShopSolution.ViewModels.Catalog.Products;
using TShopSolution.ViewModels.Common.Slide;
using System.Collections.Generic;

namespace TShopSolution.WebApp.Models
{
    public class HomeViewModel
    {
        public List<SlideViewModel> slides { get; set; }
        public List<ProductViewModel> featuredProducts { get; set; }
        public List<ProductViewModel> latestProducts { get; set; }
    }
}