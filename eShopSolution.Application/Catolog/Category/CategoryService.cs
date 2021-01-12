using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.Category;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.Application.Catolog.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly EShopDbContext _context;

        public CategoryService(EShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
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
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
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