using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Public;
using eShopSolution.ViewModels.Common;
using eShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eShopSolution.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly EShopDbContext _dbContext = null;

        public PublicProductService(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<ProductViewModel>> GetAll()
        {
            var query = from p in _dbContext.Products
                        join i in _dbContext.ProductImages on p.Id equals i.ProductId
                        select new { p, i };


            var data = await query.Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                ImageUrl = $"https://localhost:7064/image/{x.i.ImagePath}",
                Name = x.p.Name,
                Description = x.p.Description,
                Price = x.p.Price,
                Time_Created = x.p.Time_Created.Value,
                Time_Updated = x.p.Time_Updated.Value
            })
                .Take(10)
                .ToListAsync();

            return data;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request)
        {
            //1.select join
            var query = from p in _dbContext.Products
                        join pic in _dbContext.ProductInCategories on p.Id equals pic.ProductId
                        join i in _dbContext.ProductImages on p.Id equals i.ProductId
                        select new { p, i, pic };

            //2.filter
            if (request.CategoryId != null && request.CategoryId != 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            //3.Paging
            var totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize.Value)
                .Take(request.PageSize.Value)
                .Select(x => new ProductViewModel
                {
                    Id = x.p.Id,
                    ImageUrl = $"https://localhost:7064/image/{x.i.ImagePath}",
                    Name = x.p.Name,
                    Description = x.p.Description,
                    Price = x.p.Price,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>
            {
                TotalRecords = totalRow,
                items = data
            };

            return pagedResult;
        }

        public async Task<ProductViewModel> GetProductDetailById(int id)
        {
            var productDetail = await _dbContext.Products.FindAsync(id);
            var productImages = await _dbContext.ProductImages.SingleOrDefaultAsync(x => x.ProductId == productDetail.Id);

            return new ProductViewModel
            {
                Id = productDetail.Id,
                Name = productDetail.Name,
                Description = productDetail.Description,
                Price = productDetail.Price,
                ImageUrl = $"https://localhost:7064/image/{productImages.ImagePath}"
,           };
        }
    }
}
