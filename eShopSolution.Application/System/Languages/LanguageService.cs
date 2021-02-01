using TShopSolution.Data.EF;
using TShopSolution.ViewModels.Common;
using TShopSolution.ViewModels.System.Languages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tShop.Repository;

namespace TShopSolution.Application.System.Languages
{
    public class LanguageService : ILanguageService
    {
        private readonly EShopDbContext _eShopDbContext;
        private readonly UnitOfWork _context;

        public LanguageService(EShopDbContext eShopDbContext,
            UnitOfWork context)
        {
            _eShopDbContext = eShopDbContext;
            _context = context;
        }

        public async Task<ApiResult<List<LanguageVM>>> GetAll()
        {
            //var languages = await _eShopDbContext.Languages.Select(x => new LanguageVM()
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    IsDefault = x.IsDefault,
            //}).ToListAsync();
            var languages = await _context.LanguageRepository.GetQuery().Select(x => new LanguageVM()
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