using TShopSolution.ViewModels.Common.Slide;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TShopSolution.Application.Common.Slide
{
    public interface ISlideService
    {
        Task<List<SlideViewModel>> GetAll();
    }
}