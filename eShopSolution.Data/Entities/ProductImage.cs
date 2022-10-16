using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ImagePath { get; set; }

        public string Caption { get; set; }

        public DateTime Time_Created { get; set; }

        public DateTime Time_Updated { get; set; }

        public long ImageFileSize { get; set; }

        public Product Product { get; set; }
    }
}
