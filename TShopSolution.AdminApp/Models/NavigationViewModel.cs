using TShopSolution.ViewModels.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TShopSolution.AdminApp.Models
{
    public class NavigationViewModel
    {
        public List<LanguageVM> Languages { get; set; }
        public string CurrentLanguageId { get; set; }
        public string ReturnURL { get; set; }
    }
}