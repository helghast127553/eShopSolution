using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.ViewModels.Catalog.Categories.Manage;
using eShopSolution.ViewModels.Catalog.Categories;

namespace eShopSolution.Application.Catalog.Categories
{
    public interface IManageCategoryService
    {
        Task<int> Create(CategoryCreateRequest request);

        Task<int> Update(int id, CategoryUpdateRequest request);

        Task<int> Delete(int productId);

        Task<CategoryViewModel> GetById(int productId);

        Task<ApiResult<PagedResult<CategoryViewModel>>> GetAllSubCategoryPaging(GetCategoryManagePagingRequest request);

        Task<IList<CategoryViewModel>> GetAllParentCategory();
    }
}
