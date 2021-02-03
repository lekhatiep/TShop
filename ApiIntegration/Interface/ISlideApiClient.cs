using TShopSolution.ViewModels.Common;
using TShopSolution.ViewModels.Common.Slide;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TShopSolution.ApiIntegration.Interface
{
    public interface ISlideApiClient
    {
        Task<List<SlideViewModel>> GetAll();
    }
}