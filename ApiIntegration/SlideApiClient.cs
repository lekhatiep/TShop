using TShopSolution.ApiIntegration.Interface;
using TShopSolution.ViewModels.Common.Slide;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TShopSolution.ApiIntegration
{
    public class SlideApiClient : BaseApiClient, ISlideApiClient
    {
        public SlideApiClient(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor) { }

        public async Task<List<SlideViewModel>> GetAll()
        {
            var data = await GetListAsync<SlideViewModel>("api/slides");
            return data;
        }
    }
}