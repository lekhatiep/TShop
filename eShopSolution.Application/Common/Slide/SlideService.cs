using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Common.Slide;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.Application.Common.Slide
{
    public class SlideService : ISlideService
    {
        private readonly EShopDbContext _eShopDbContext;

        public SlideService(EShopDbContext eShopDbContext)
        {
            _eShopDbContext = eShopDbContext;
        }

        public async Task<List<SlideViewModel>> GetAll()
        {
            var slides = await _eShopDbContext.Slides.OrderByDescending(x => x.SortOrder)
                .Select(x => new SlideViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    Url = x.Url
                }).ToListAsync();
            return slides;
        }
    }
}