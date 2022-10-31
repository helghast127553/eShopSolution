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

        public decimal Price { get; set; }

        public DateTime? Time_Created { get; set; }

        public DateTime? Time_Updated { get; set; }

        public int CategoryId { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public List<Cart> Carts { get; set; }

        public List<ProductImage> ProductImages { get; set; }

        public List<ProductRating> ProductRatings { get; set; }

        public Category Category { get; set; }
    }
}
