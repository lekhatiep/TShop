using TShopSolution.ViewModels.System.Languages;
using System.Collections.Generic;

namespace TShopSolution.WebApp.Models
{
    public class NavigationViewModel
    {
        public List<LanguageVM> Languages { get; set; }
        public string CurrentLanguageId { get; set; }
        public string ReturnURL { get; set; }
    }
}