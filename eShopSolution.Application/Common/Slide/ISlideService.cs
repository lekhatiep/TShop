using eShopSolution.ViewModels.Common.Slide;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Common.Slide
{
    public interface ISlideService
    {
        Task<List<SlideViewModel>> GetAll();
    }
}