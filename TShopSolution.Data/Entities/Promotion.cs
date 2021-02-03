using TShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace TShopSolution.Data.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool ApplyForAll { get; set; }
        public int? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int ProductCategoriesIds { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }

    }
}
