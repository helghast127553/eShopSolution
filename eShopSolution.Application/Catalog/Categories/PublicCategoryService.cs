using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Categories
{
    public class PublicCategoryService : IPublicCategoryService
    {
        private readonly EShopDbContext _context;

        public PublicCategoryService(EShopDbContext context)
        {
            _context = context;
        }

        public async Task<IList<CategoryViewModel>> GetAll()
        {
            return await _context.Categories
                .Select(x => new CategoryViewModel
                { 
                    Id = x.Id,
                    Name = x.Name, 
                    ParentId = x.ParentId 
                })
                .ToListAsync();
        }

        public async Task<CategoryViewModel> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return new CategoryViewModel { Id = category.Id, Name = category.Name };
        }
    }
}
