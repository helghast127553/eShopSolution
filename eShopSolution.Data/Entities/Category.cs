using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Entities
{ 
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? SeoDescription { set; get; }

        public string? SeoTitle { set; get; }

        public string? SeoAlias { set; get; }

        public string Description { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? ParentId { get; set; }

        public List<Product> Products { get; set; }
    }
}
