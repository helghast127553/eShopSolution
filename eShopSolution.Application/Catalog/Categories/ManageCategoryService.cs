using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Categories.Manage;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
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
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);

            if (category == null)
            {
                return -1;
            }

            _dbContext.Remove(category);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<CategoryViewModel>> GetAllSubCategory()
        {
            return await _dbContext.Categories
                .Where(x => x.ParentId != null)
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ParentId = x.ParentId,
                    Time_Created = x.Time_Created,
                    Time_Updated = x.Time_Updated,
                }).ToListAsync();
        }

        public async Task<ApiResult<PagedResult<CategoryViewModel>>> GetAllSubCategoryPaging(GetCategoryManagePagingRequest request)
        {
            var query = _dbContext.Categories.Where(x => x.ParentId != null);

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize.Value)
                .Take(request.PageSize.Value)
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ParentId = x.ParentId,
                    Time_Created = x.Time_Created,
                    Time_Updated = x.Time_Updated,
                }).ToListAsync();

            var pagedResult = new PagedResult<CategoryViewModel>
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize.Value,
                items = data
            };

            return new ApiSuccessResult<PagedResult<CategoryViewModel>>(pagedResult);
        }

        public async Task<IList<CategoryViewModel>> GetAllParentCategory()
        {
            return await _dbContext.Categories
                .Where(x => x.ParentId == null)
                .Select(x => new CategoryViewModel { Id = x.Id, Name = x.Name, Description = x.Description })
                .ToListAsync();
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

        public async Task<int> Update(int id, CategoryUpdateRequest request)
        {
            var category = await _dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                return -1;
            }

            category.Name = request.Name;
            category.Description = request.Description;
            category.Time_Updated = DateTime.Now;
            category.ParentId = request.ParentId;

            return await _dbContext.SaveChangesAsync();
        }
    }
}
