using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Categories.Manage;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Categories
{
    public class ManageCategoryService : IManageCategoryService
    {
        private readonly EShopDbContext _dbContext = null;

        public ManageCategoryService(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(CategoryCreateRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                Time_Created = DateTime.Now,
                ParentId = request.ParentId,
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category.Id;
        }

        public async Task<int> Delete(int categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);

            if (category == null)
            {
                throw new EShopException($"Cannot find category data: {categoryId}");
            }

            _dbContext.Remove(category);

            return await _dbContext.SaveChangesAsync();
        }

        public Task<ApiResult<PagedResult<CategoryViewModel>>> GetAllPaging(GetCategoryManagePagingRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryViewModel> GetById(int categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);

            var categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                Description = category.Description,
                Name = category.Name,
            };

            return categoryViewModel;
        }

        public async Task<int> Update(CategoryUpdateRequest request)
        {
            var category = await _dbContext.Categories.FindAsync(request.Id);

            if (category == null)
            {
                throw new EShopException($"Cannot find a category with id: {request.Id}");
            }

            category.Name = request.Name;
            category.Description = request.Description;
            category.Time_Updated = DateTime.Now;
            category.ParentId = request.ParentId;

            return await _dbContext.SaveChangesAsync();
        }
    }
}
