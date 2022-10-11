﻿using eShopSolution.ViewModels.Catalog.Products;
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
                        join c in _dbContext.Categories on p.CategoryId equals c.Id
                        join i in _dbContext.ProductImages on p.Id equals i.ProductId
                        select new { p, i };

            var data = await query.Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                DateCreated = x.p.DateCreated.Value,
                Description = x.p.Description,
                Details = x.p.Details,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
                SeoAlias = x.p.SeoAlias,
                SeoDescription = x.p.SeoDescription,
                SeoTitle = x.p.SeoTitle,
                Stock = x.p.Stock,
                ViewCount = x.p.ViewCount,
                ImageUrl = $"https://localhost:7064/image/{x.i.ImagePath}"
            }).ToListAsync();

            return data;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request)
        {
            //1.select join
            var query = from p in _dbContext.Products
                        join c in _dbContext.Categories on p.CategoryId equals c.Id
                        select new { p };

            //2.filter
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.p.CategoryId == request.CategoryId);
            }

            //3.Paging
            var totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    DateCreated = x.p.DateCreated.Value,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.p.SeoAlias,
                    SeoDescription = x.p.SeoDescription,
                    SeoTitle = x.p.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>
            {
                TotalRecord = totalRow,
                items = data
            };

            return pagedResult;
        }
    }
}
