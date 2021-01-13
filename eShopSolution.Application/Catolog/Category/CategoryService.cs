using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.Category;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tShop.Repository;

namespace eShopSolution.Application.Catolog.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            var query = from c in _unitOfWork.CategoryRepository.GetQuery()
                        join ct in _unitOfWork.CategoryTranslationRepository.GetQuery() on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };

            var data = await query.Select(x => new CategoryViewModel
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId
            }).ToListAsync();

            return data;
        }

        public async Task<CategoryViewModel> GetCategoryById(int id, string languageId)
        {
            var query = from c in _unitOfWork.CategoryRepository.GetQuery()
                        join ct in _unitOfWork.CategoryTranslationRepository.GetQuery() on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        && ct.CategoryId == id
                        select new { c, ct };
            var data = await query.Select(x => new CategoryViewModel
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId
            }).FirstOrDefaultAsync();

            return data;
        }
    }
}