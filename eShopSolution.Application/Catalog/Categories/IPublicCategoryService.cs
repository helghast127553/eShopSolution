using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Categories
{
    public interface IPublicCategoryService
    {
        Task<IList<CategoryViewModel>> GetAll();

        Task<CategoryViewModel> GetById(int id);


    }
}
