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
using eShopSolution.ViewModels.System.Users;
using System.Security.Cryptography;

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
                Time_Created = DateTime.Now,
                Name = request.Name,
                Description = request.Description,
                ProductInCategories = new List<ProductInCategory>
                {
                    new ProductInCategory { CategoryId = request.CategoryId }
                }
            };

            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>
                {
                    new ProductImage
                    {

                        ImageFileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        Caption = request.Name,
                        Time_Created = DateTime.Now,
                    }
                };
            }

            _dbContext.Products.Add(product);
          
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                throw new EShopException($"Cannot find a product: {id}");
            }

            var images = _dbContext.ProductImages.Where(x => x.ProductId == id);

            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _dbContext.Remove(product);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<ApiResult<PagedResult<ProductViewModel>>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1.select join
            var query = from p in _dbContext.Products
                        join pic in _dbContext.ProductInCategories on p.Id equals pic.ProductId
                        join c in _dbContext.Categories on pic.CategoryId equals c.Id
                        join pi in _dbContext.ProductImages on p.Id equals pi.ProductId
                        select new { p, pic, c, pi };

            //2.Paging
            var totalRow = await query.CountAsync();
            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize.Value)
                .Take(request.PageSize.Value)
                .Select(x => new ProductViewModel
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    CategoryName = x.c.Name,
                    Description = x.p.Description,
                    Price = x.p.Price,
                    ImageUrl = $"https://localhost:7064/image/{x.pi.ImagePath}",
                    Time_Created = x.p.Time_Created != null ? x.p.Time_Created.Value : null,
                    Time_Updated = x.p.Time_Updated != null ? x.p.Time_Updated.Value : null
                }).ToListAsync();

            //3. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize.Value,
                items = data
            };

            return new ApiSuccessResult<PagedResult<ProductViewModel>>(pagedResult);
        }


        public async Task<ProductViewModel> GetById(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);

            var productViewModel = new ProductViewModel 
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
            };

            return productViewModel;
        }

        public async Task<int> Update(int id, ProductUpdateRequest request)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                throw new EShopException($"Cannot find a product with id: {id}");
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            product.Time_Updated = DateTime.Now;

            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _dbContext.ProductImages
                    .FirstOrDefaultAsync(i => i.ProductId == id);

                if (thumbnailImage != null)
                {
                    thumbnailImage.ImageFileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    thumbnailImage.Caption = request.Name;
                    thumbnailImage.Time_Updated = DateTime.Now;
                    _dbContext.ProductImages.Update(thumbnailImage);
                }
            }

            var productInCategories = _dbContext.ProductInCategories.SingleOrDefault(x => x.ProductId == id);
            productInCategories.CategoryId = request.CategoryId;

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
