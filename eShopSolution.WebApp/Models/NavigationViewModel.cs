using eShopSolution.ViewModels.System.Languages;
using System.Collections.Generic;

namespace eShopSolution.WebApp.Models
{
    public class NavigationViewModel
    {
        public List<LanguageVM> Languages { get; set; }
        public string CurrentLanguageId { get; set; }
        public string ReturnURL { get; set; }
    }
}