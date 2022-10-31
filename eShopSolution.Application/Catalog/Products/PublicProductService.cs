using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Public;
using eShopSolution.ViewModels.Common;
using eShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using eShopSolution.Data.Configurations;

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
            var productImages = await _dbContext.ProductImages.SingleOrDefaultAsync(x => x.ProductId == productDetail.Id);

            return new ProductViewModel
            {
                Id = productDetail.Id,
                Name = productDetail.Name,
                Description = productDetail.Description,
                Price = productDetail.Price,
                ImageUrl = $"https://localhost:7064/image/{productImages.ImagePath}"
            };
        }
    }
}
