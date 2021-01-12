using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Common.Slide;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration.Interface
{
    public interface ISlideApiClient
    {
        Task<List<SlideViewModel>> GetAll();
    }
}