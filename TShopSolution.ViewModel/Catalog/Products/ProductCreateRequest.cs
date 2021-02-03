using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TShopSolution.ViewModels.Catalog.Products
{
    public class ProductCreateRequest
    {
        [Display(Name = "Giá bán")]
        public decimal Price { get; set; }

        [Display(Name = "Giá gốc")]
        public decimal Original { get; set; }

        [Display(Name = "Tồn kho")]
        public int Stock { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        public string Description { get; set; }

        [Display(Name = "Chi tiết sản phẩm")]
        public string Details { get; set; }

        [Display(Name = "SEO mô tả")]
        public string SeoDescription { get; set; }

        [Display(Name = "SEO tiêu đề")]
        public string SeoTitle { get; set; }

        [Display(Name = "SEO alias")]
        public string SeoAlias { get; set; }

        public string LanguageId { get; set; }
        public bool? IsFeatured { get; set; }

        [Display(Name = "Hình ảnh sản phẩm")]
        public IFormFile ThumbnailImage { get; set; }
    }
}