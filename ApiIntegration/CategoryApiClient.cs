using TShopSolution.ApiIntegration.Interface;
using TShopSolution.ViewModels.Catalog.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TShopSolution.ApiIntegration
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        : base(httpClientFactory, configuration, httpContextAccessor) { }

        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            return await GetListAsync<CategoryViewModel>("api/category?languageId=" + languageId);
        }

        public async Task<CategoryViewModel> GetCategoryById(int id, string languageId)
        {
            return await GetAsync<CategoryViewModel>($"api/category/{id}/{languageId}");
        }
    }
}