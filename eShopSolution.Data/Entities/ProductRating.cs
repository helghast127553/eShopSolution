using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Entities
{
    public class ProductRating
    {
        public int Id { get; set; }

        public float Rating { get; set; }

        public string? Review { get; set; }

        public Guid UserId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public AppUser AppUser { get; set; }

        public DateTime Time_Created { get; set; }
    }
}
