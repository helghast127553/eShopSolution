﻿using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly EShopDbContext _context;

        public CategoryService(EShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            return await _context.Categories
                .Select(x => new CategoryViewModel { Id = x.Id, Name = x.Name })
                .ToListAsync();
        }

        public async Task<CategoryViewModel> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            var categoryViewModel = new CategoryViewModel { Id = category.Id, Name = category.Name };
            return categoryViewModel;
        }
    }
}