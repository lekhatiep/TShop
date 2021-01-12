using eShopSolution.ViewModels.Catalog.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catolog.Category
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll(string languageId);

        Task<CategoryViewModel> GetCategoryById(int Id, string languageId);
    }
}