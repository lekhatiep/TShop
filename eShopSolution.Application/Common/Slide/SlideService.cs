using TShopSolution.ViewModels.Common.Slide;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tShop.Repository;

namespace TShopSolution.Application.Common.Slide
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