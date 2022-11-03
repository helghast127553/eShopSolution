using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Public;
using eShopSolution.ViewModels.Common;
using eShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using eShopSolution.Data.Configurations;
using eShopSolution.Data.Entities;

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

            return await query.Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                ImageUrl = $"https://localhost:7064/image/{x.i.ImagePath}",
                Name = x.p.Name,
                Description = x.p.Description,
                Price = x.p.Price,
                CategoryId = x.p.CategoryId,
                Rating = _dbContext.ProductRatings.Any(pr => pr.ProductId == x.p.Id)
                ? (int)_dbContext.ProductRatings.Where(pr => pr.ProductId == x.p.Id).Average(x => x.Rating) : 0,
                Time_Created = x.p.Time_Created.Value,
                Time_Updated = x.p.Time_Updated.Value
            })
                .Take(10)
                .ToListAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryIdPaging(GetPublicProductPagingRequest request)
        {
            //1.select join
            var query = from p in _dbContext.Products
                        join i in _dbContext.ProductImages on p.Id equals i.ProductId
                        join c in _dbContext.Categories on p.CategoryId equals c.Id
                        select new { p, i, c };

            //2.filter
            if (request.SubCategoryId != null && request.SubCategoryId.Value != 0)
            {
                query = query.Where(p => p.p.CategoryId == request.SubCategoryId.Value);
            }

            if (request.ParentCategoryId != null && request.ParentCategoryId.Value != 0)
            {
                query = query.Where(p => p.c.ParentId == request.ParentCategoryId.Value);
            }

            //paging
            var totalRow = await query.CountAsync();

            var data = await query
                .Take(request.PageSize.Value * request.PageIndex)
                .Select(x => new ProductViewModel
                {
                    Id = x.p.Id,
                    ImageUrl = $"https://localhost:7064/image/{x.i.ImagePath}",
                    Name = x.p.Name,
                    Description = x.p.Description,
                    Price = x.p.Price,
                    CategoryId = x.p.CategoryId,
                    Rating = _dbContext.ProductRatings.Any(pr => pr.ProductId == x.p.Id)
                ? (int)_dbContext.ProductRatings.Where(pr => pr.ProductId == x.p.Id).Average(x => x.Rating) : 0,
                }).ToListAsync();

            //3. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize.Value,
                items = data,
                TotalRecords = totalRow,
            };

            return pagedResult;
        }

        public async Task<ProductViewModel> GetProductDetailById(int id)
        {
            var productDetail = await _dbContext.Products.FindAsync(id);
            var productImages = await _dbContext.ProductImages.SingleOrDefaultAsync(x => x.ProductId == id);

            return new ProductViewModel
            {
                Id = productDetail.Id,
                Name = productDetail.Name,
                Description = productDetail.Description,
                Price = productDetail.Price,
                Rating = _dbContext.ProductRatings.Any(x => x.ProductId == id)
                ? (int)_dbContext.ProductRatings.Where(x => x.ProductId == id).Average(x => x.Rating) : 0,
                ImageUrl = $"https://localhost:7064/image/{productImages.ImagePath}",
                CategoryId = productDetail.CategoryId,
            };
        }

        public async Task<IList<ProductViewModel>> GetRelatedProductsByCategoryId(int categoryId)
        {
            return await _dbContext.Products.Where(x => x.CategoryId == categoryId)
                .Select(x => new ProductViewModel 
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Rating = _dbContext.ProductRatings.Any(x => x.ProductId == x.Id)
                ? (int)_dbContext.ProductRatings.Where(x => x.ProductId == x.Id).Average(x => x.Rating) : 0,
                    ImageUrl = $"https://localhost:7064/image/{_dbContext.ProductImages.SingleOrDefault(pi => pi.ProductId == x.Id).ImagePath}",
                    CategoryId = x.CategoryId,
                })
                .Take(8)
                .ToListAsync();
        }
    }
}
