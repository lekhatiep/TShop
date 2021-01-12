using eShopSolution.Utilities.Constant;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public class BaseApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseApiClient(
              IHttpClientFactory httpClientFactory
            , IConfiguration configuration
            , IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        protected async Task<T> GetAsync<T>(string requestUri)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSettings.BaseAddress]);
            var sessionToken = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.Token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);
            var respone = await client.GetAsync(requestUri);

            if (respone.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await respone.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<T>(await respone.Content.ReadAsStringAsync());
        }

        protected async Task<List<T>> GetListAsync<T>(string requestUri)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSettings.BaseAddress]);
            var sessionToken = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.Token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);
            var respone = await client.GetAsync(requestUri);
            var body = await respone.Content.ReadAsStringAsync();

            if (respone.IsSuccessStatusCode)
            {
                return (List<T>)JsonConvert.DeserializeObject(body, typeof(List<T>));
            }
            throw new Exception(body);
        }

        protected async Task<T> PostAsync<T>(string Uri, T request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpContent = new StringContent(request.ToString(), Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSettings.BaseAddress]);
            var sessionToken = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.Token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);
            using var response = await client.PostAsync($"{Uri}", httpContent);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> UpdateAsync<T>(Guid Id, string Uri, T request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            var sessionToken = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            using var response = await client.PutAsync($"{Uri}/{Id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<T>(result);

            return JsonConvert.DeserializeObject<T>(result);
        }

        protected async Task<bool> DeleteAsync<T>(string Uri, string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessionToken = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);
            using var response = await client.DeleteAsync($"{Uri}/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}