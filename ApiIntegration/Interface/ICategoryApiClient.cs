using TShopSolution.ViewModels.Catalog.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TShopSolution.ApiIntegration.Interface
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryViewModel>> GetAll(string languageId);

        Task<CategoryViewModel> GetCategoryById(int id, string languageId);
    }
}