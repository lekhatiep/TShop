using TShopSolution.ApiIntegration.Interface;
using TShopSolution.ApiIntegration;
using TShopSolution.ViewModels.Common;
using TShopSolution.ViewModels.System.Languages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TShopSolution.ApiIntegration
{
    public class LanguageApiClient : BaseApiClient, ILanguageApiClient
    {
        public LanguageApiClient(
              IHttpClientFactory httpClientFactory
            , IConfiguration configuration
            , IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor) { }

        public async Task<ApiResult<List<LanguageVM>>> GetAll()
        {
            return await GetAsync<ApiResult<List<LanguageVM>>>("api/languages");
        }
    }
}