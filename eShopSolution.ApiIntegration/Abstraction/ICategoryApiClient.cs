using eShopSolution.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration.Abstraction
{
    public interface ICategoryApiClient
    {
        Task<IList<CategoryViewModel>> GetAll();
    }
}