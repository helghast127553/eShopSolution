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

        public string Description { get; set; }

        public DateTime? Time_Created { get; set; }

        public DateTime? Time_Updated { get; set; }

        public int? ParentId { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }

        public List<Product> Products { get; set; }
    }
}
