using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.ProductRating
{
    public class ProductRatingCreateRequest
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Review { get; set; }

        public int Rating { get; set; }

        public string Username { get; set; }
    }
}
