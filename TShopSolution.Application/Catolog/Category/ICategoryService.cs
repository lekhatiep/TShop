using TShopSolution.ViewModels.Catalog.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TShopSolution.Application.Catolog.Category
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll(string languageId);

        Task<CategoryViewModel> GetCategoryById(int Id, string languageId);
    }
}