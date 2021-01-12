using eShopSolution.ViewModels.Catalog.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration.Interface
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryViewModel>> GetAll(string languageId);

        Task<CategoryViewModel> GetCategoryById(int id, string languageId);
    }
}