using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(int id, ProductUpdateRequest request);

        Task<int> Delete(int id);

        Task<ProductViewModel> GetById(int productId);

        Task<ApiResult<PagedResult<ProductViewModel>>> GetAllProductPaging(GetManageProductPagingRequest request);
    }
}
