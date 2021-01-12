using eShopSolution.ApiIntegration.Interface;
using eShopSolution.Utilities.Constant;
using eShopSolution.ViewModels.Catalog.Category;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductApiClient(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        : base(httpClientFactory, configuration, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateProduct(ProductCreateRequest request)
        {
            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.DefaultLanguageId);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSettings.BaseAddress]);
            var sessionToken = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.Token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);
            var requestcontent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;

                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestcontent.Add(bytes, "ThumbnailImage", request.ThumbnailImage.FileName);
            }
            requestcontent.Add(new StringContent(request.Price.ToString()), "price");
            requestcontent.Add(new StringContent(request.Name.ToString()), "Name");
            requestcontent.Add(new StringContent(request.Original.ToString()), "Original");
            requestcontent.Add(new StringContent(request.Stock.ToString()), "Stock");
            requestcontent.Add(new StringContent(request.Name.ToString()), "Description");
            requestcontent.Add(new StringContent(request.Details.ToString()), "Details");
            requestcontent.Add(new StringContent(request.SeoDescription.ToString()), "SeoDescription");
            requestcontent.Add(new StringContent(request.SeoTitle.ToString()), "SeoTitle");
            requestcontent.Add(new StringContent(request.SeoAlias.ToString()), "SeoAlias");
            requestcontent.Add(new StringContent(languageId), "LanguageId");

            var response = await client.PostAsync("/api/products/create", requestcontent);
            return response.IsSuccessStatusCode;
        }

        public async Task<ProductViewModel> GetById(int Id, string languageId)
        {
            var data = await GetAsync<ProductViewModel>(
               $"/api/products/{Id}/{languageId}");
            return data;
        }

        public async Task<ApiResult<PagedResult<ProductViewModel>>> GetProductPaging(GetManageProductPagingRequest request)
        {
            var data = await GetAsync<ApiResult<PagedResult<ProductViewModel>>>(
               $"/api/products/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}&languageId={request.LanguageId}&categoryId={request.CategoryId}");

            return data;
        }

        public async Task<ApiResult<bool>> AssginCategory(int Id, CategoryAssignRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            var sessionToken = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            using var response = await client.PutAsync($"/api/products/{Id}/categories", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<ProductViewModel>> GetFeaturedProduct(string languageId, int take)
        {
            var data = await GetListAsync<ProductViewModel>($"/api/products/featured/{languageId}/{take}");
            return data;
        }

        public async Task<List<ProductViewModel>> GetLatestProduct(string languageId, int take)
        {
            var data = await GetListAsync<ProductViewModel>($"/api/products/latest/{languageId}/{take}");
            return data;
        }

        public async Task<bool> UpdateProduct(ProductUpdateRequest request)
        {
            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.DefaultLanguageId);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSettings.BaseAddress]);
            var sessionToken = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.Token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);
            var requestcontent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;

                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestcontent.Add(bytes, "ThumbnailImage", request.ThumbnailImage.FileName);
            }
            requestcontent.Add(new StringContent(request.Id.ToString()), "Id");
            requestcontent.Add(new StringContent(request.Name.ToString()), "Name");
            requestcontent.Add(new StringContent(request.Description.ToString()), "Description");
            requestcontent.Add(new StringContent(request.Details.ToString()), "Details");
            requestcontent.Add(new StringContent(request.SeoDescription.ToString()), "SeoDescription");
            requestcontent.Add(new StringContent(request.SeoTitle.ToString()), "SeoTitle");
            requestcontent.Add(new StringContent(request.SeoAlias.ToString()), "SeoAlias");
            requestcontent.Add(new StringContent(languageId), "LanguageId");

            var response = await client.PutAsync("/api/products", requestcontent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var data = await DeleteAsync<bool>(
              $"/api/products", productId.ToString());
            return data;
        }
    }
}