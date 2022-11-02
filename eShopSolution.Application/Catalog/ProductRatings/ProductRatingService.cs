using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.ProductRating;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.ProductRatings
{
    public class ProductRatingService : IProductRatingService
    {
        private readonly EShopDbContext _dbContext = null;
        private readonly UserManager<AppUser> _userManager;

        public ProductRatingService(EShopDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<int> Create(ProductRatingCreateRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            var rating = new ProductRating
            {
                Name = request.Name,
                Review = request.Review,
                Rating = request.Rating,
                ProductId = request.ProductId,
                UserId = user.Id,
                Time_Created = DateTime.Now
            };

            await _dbContext.ProductRatings.AddAsync(rating);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<ProductRatingViewModel>> GetAllProductRatingsByProductId(int id)
        {

            return await _dbContext.ProductRatings
                .Where(x => x.ProductId == id)
                .Select(x => new ProductRatingViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Review = x.Review,
                    Rating = x.Rating,
                    ProductId = x.ProductId,
                    UserId = x.UserId,
                    TimeCreated = x.Time_Created
                }).ToListAsync();
        }

        public async Task<int> Delete(int id)
        {
            var productRating = await _dbContext.ProductRatings.FindAsync(id);

            if (productRating == null)
            {
                return -1;
            }

            _dbContext.Remove(productRating);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
