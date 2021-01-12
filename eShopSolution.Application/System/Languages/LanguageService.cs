using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Languages
{
    public class LanguageService : ILanguageService
    {
        private readonly EShopDbContext _eShopDbContext;

        public LanguageService(EShopDbContext eShopDbContext)
        {
            _eShopDbContext = eShopDbContext;
        }

        public async Task<ApiResult<List<LanguageVM>>> GetAll()
        {
            var languages = await _eShopDbContext.Languages.Select(x => new LanguageVM()
            {
                Id = x.Id,
                Name = x.Name,
                IsDefault = x.IsDefault,
            }).ToListAsync();
            if (languages != null)
            {
                return new ApiSuccessResult<List<LanguageVM>>(languages);
            }
            return new ApiErrorResult<List<LanguageVM>>("Not found");
        }
    }
}