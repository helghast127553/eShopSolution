using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.ProductRating;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Public;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration.Abstraction
{
    public interface IProductApiClient
    {
        Task<int> CreateRating(ProductRatingCreateRequest request);

        Task<int> DeleteRating(int id);

        Task<IList<ProductRatingViewModel>> GetAllProductRatingByProductId(int id);

        Task<IList<ProductViewModel>> GetAll();

        Task<ProductViewModel> GetProductDetail(int id);

        Task<PagedResult<ProductViewModel>> GetAllProductsByCategory(int subCategoryId = 0, int parentCategoryId = 0, int pageIndex = 1, int pageSize = 15);
    }
}
