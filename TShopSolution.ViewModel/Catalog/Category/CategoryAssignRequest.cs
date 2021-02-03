using TShopSolution.ViewModels.System.Users;
using System.Collections.Generic;

namespace TShopSolution.ViewModels.Catalog.Category
{
    public class CategoryAssignRequest
    {
        public int Id { get; set; }
        public List<SelectedItem> Categories { get; set; } = new List<SelectedItem>();
    }
}