using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration.Abstraction
{
    public interface IProductApiClient
    {
        Task<List<ProductViewModel>> GetAll();
    }
}
