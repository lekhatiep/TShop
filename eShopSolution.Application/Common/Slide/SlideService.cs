using eShopSolution.Data.EF;
using eShopSolution.Data.Repositories;
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
        private readonly UnitOfWork _unitOfWork;

        public SlideService(
            UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SlideViewModel>> GetAll()
        {
            //var slides = await _eShopDbContext.Slides.OrderByDescending(x => x.SortOrder)
            //    .Select(x => new SlideViewModel()
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        Description = x.Description,
            //        Image = x.Image,
            //        Url = x.Url
            //    }).ToListAsync();
            var slideQuery = await _unitOfWork.SlideRepository.GetAsync(orderBy: x => x.OrderByDescending(x => x.SortOrder));
            var slides = slideQuery.Select(x => new SlideViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Image = x.Image,
                Url = x.Url
            }).ToList();

            return slides;
        }
    }
}