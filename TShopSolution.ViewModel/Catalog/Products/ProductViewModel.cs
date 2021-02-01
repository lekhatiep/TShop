using TShopSolution.ViewModels.Catalog.Category;
using TShopSolution.ViewModels.Catalog.ProductImages;
using System;
using System.Collections.Generic;
using System.Text;

namespace TShopSolution.ViewModels.Catalog.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Original { get; set; }
        public int Stock { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoAlias { get; set; }
        public string LanguageId { get; set; }
        public bool? IsFeatured { get; set; }
        public string Image { get; set; }
        public List<ProductImageViewModel> OtherImages { get; set; }

        public List<string> Categories { get; set; }
    }
}