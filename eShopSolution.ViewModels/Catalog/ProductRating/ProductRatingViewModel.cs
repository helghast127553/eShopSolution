using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.ProductRating
{
    public class ProductRatingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Rating { get; set; }

        public string Review { get; set; }

        public Guid UserId { get; set; }

        public int ProductId { get; set; }

        public DateTime TimeCreated { get; set; }
    }
}
