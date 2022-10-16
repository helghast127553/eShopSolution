using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Products.Manage
{
    public class ProductCreateRequest
    {
        public string Name { set; get; }

        public decimal Price { get; set; }

        public string Description { set; get; }

        public int CategoryId { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }
}
