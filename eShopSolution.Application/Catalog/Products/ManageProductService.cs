using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using eShopSolution.Application.Common;
using eShopSolution.ViewModels.Catalog.Products.Public;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _dbContext;
        private readonly IStorageService _storageService;

        public ManageProductService(EShopDbContext dbContext, IStorageService storageService)
        {
            _dbContext = dbContext;
            _storageService = storageService;
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                Name = request.Name,
                Description = request.Description,
                Details = request.Details,
                SeoDescription = request.SeoDescription,
                SeoAlias = request.SeoAlias,
                SeoTitle = request.SeoTitle,
                CategoryId = request.CategoryId
            };

            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>
                {
                    new ProductImage
                    {
                        Caption = request.Name,
                        DateCreated = DateTime.Now,
                        ImageFileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);

            if (product == null)
            {
                throw new EShopException($"Cannot find a product: {productId}");
            }

            var images = _dbContext.ProductImages.Where(x => x.ProductId == productId);

            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _dbContext.Remove(product);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1.select join
            var query = from p in _dbContext.Products
                        join c in _dbContext.Categories on p.CategoryId equals c.Id
                        select new { p };

            //2.filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.Name.Contains(request.Keyword));
            }

            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.p.CategoryId));
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


        public async Task<ProductViewModel> GetById(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);

            var productViewModel = new ProductViewModel 
            {
                Id = product.Id,
                Description = product != null ? product.Description : null,
                Details = product != null ? product.Details : null,
                Name = product != null ? product.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = product != null ? product.SeoAlias : null,
                SeoDescription = product != null ? product.SeoDescription : null,
                SeoTitle = product != null ? product.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };

            return productViewModel;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _dbContext.Products.FindAsync(request.Id);

            if (product == null)
            {
                throw new EShopException($"Cannot find a product with id: {request.Id}");
            }

            product.Name = request.Name;
            product.OriginalPrice = request.OriginalPrice;
            product.Price = request.Price;
            product.SeoAlias = request.SeoAlias;
            product.SeoDescription = request.SeoDescription;
            product.SeoTitle = request.SeoTitle;
            product.Description = request.Description;
            product.Details = request.Details;

            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _dbContext.ProductImages
                    .FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);

                if (thumbnailImage != null)
                {
                    thumbnailImage.ImageFileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    thumbnailImage.Caption = request.Name;
                    _dbContext.ProductImages.Update(thumbnailImage);
                }
            }

            return await _dbContext.SaveChangesAsync();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
