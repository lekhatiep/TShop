using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common.Slide;
using System.Collections.Generic;

namespace eShopSolution.WebApp.Models
{
    public class HomeViewModel
    {
        public List<SlideViewModel> slides { get; set; }
        public List<ProductViewModel> featuredProducts { get; set; }
        public List<ProductViewModel> latestProducts { get; set; }
    }
}