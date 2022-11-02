using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { set; get; }

        public string CategoryName { set; get; }

        public int CategoryId { set; get; }

        public string Description { set; get; }

        public decimal Price { get; set; }

        public double Rating { get; set; }

        public DateTime? Time_Created { get; set; }

        public DateTime? Time_Updated { get; set; }

        public string ImageUrl { get; set; }
    }
}
