using eShopSolution.ApiIntegration.Abstraction;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Public;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IList<ProductViewModel>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync("api/product/");
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(body, typeof(List<ProductViewModel>)) as List<ProductViewModel>;
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
