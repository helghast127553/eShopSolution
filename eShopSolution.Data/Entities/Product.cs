using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { set; get; }

        public string Description { set; get; }

        public string? Details { set; get; }

        public string? SeoDescription { set; get; }

        public string? SeoTitle { set; get; }

        public string? SeoAlias { get; set; }

        public decimal Price { get; set; }

        public decimal OriginalPrice { get; set; }

        public int Stock { get; set; }

        public int ViewCount { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public List<Cart> Carts { get; set; }

        public List<ProductImage> ProductImages { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
