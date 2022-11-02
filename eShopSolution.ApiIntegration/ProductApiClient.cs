using eShopSolution.ApiIntegration.Abstraction;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.ProductRating;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Public;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, 
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<int> CreateRating(ProductRatingCreateRequest request)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[SystemConstants.Token];
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/product/rating/", httpContent);

            return response.IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeleteRating(int id)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[SystemConstants.Token];
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"api/product/rating/{id}/");

            return response.IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<IList<ProductViewModel>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync("api/product/");
            var body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject(body, typeof(List<ProductViewModel>)) as List<ProductViewModel>;
        }

        public async Task<IList<ProductRatingViewModel>> GetAllProductRatingByProductId(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"api/product/rating/{id}/");
            var body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject(body, typeof(List<ProductRatingViewModel>)) as List<ProductRatingViewModel>;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllProductsByCategory(int subCategoryId = 0, int parentCategoryId = 0, int pageIndex = 1, int pageSize = 15)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var query = new Dictionary<string, string>()
            {
                ["SubCategoryId"] = subCategoryId.ToString(),
                ["ParentCategoryId"] = parentCategoryId.ToString(),
                ["PageIndex"] = pageIndex.ToString(),
                ["PageSize"] = pageSize.ToString()
            };

            var url =  QueryHelpers.AddQueryString("api/product/paging/", query);

            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject(body, typeof(PagedResult<ProductViewModel>)) as PagedResult<ProductViewModel>;
        }

        public async Task<ProductViewModel> GetProductDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"api/product/detail/{id}/");
            var body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject(body, typeof(ProductViewModel)) as ProductViewModel;
        }
    }
}
