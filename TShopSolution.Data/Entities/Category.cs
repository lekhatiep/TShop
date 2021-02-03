using TShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace TShopSolution.Data.Entities
{
    public class Category
    {
        public int Id {get;set;}
        public int SortOrDer {get;set;}
        public bool IsShowOnHome {get;set;}
        public int? ParentId {get;set;}
        public Status Status { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
