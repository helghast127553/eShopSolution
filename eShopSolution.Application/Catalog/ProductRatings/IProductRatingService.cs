using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.ProductRating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.ProductRatings
{
    public interface IProductRatingService
    {
        Task<int> Create(ProductRatingCreateRequest request);

        Task<IList<ProductRatingViewModel>> GetAllProductRatingsByProductId(int id);
    }
}
