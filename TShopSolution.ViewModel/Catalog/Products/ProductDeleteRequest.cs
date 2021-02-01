using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TShopSolution.ViewModels.Catalog.Products
{
    public class ProductDeleteRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}