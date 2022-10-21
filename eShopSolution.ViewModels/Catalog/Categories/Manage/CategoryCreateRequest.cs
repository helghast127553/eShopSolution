using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Categories.Manage
{
    public class CategoryCreateRequest
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int ParentId { get; set; }


        public DateTime? Time_Created { get; set; }

        public DateTime? Time_Updated { get; set; }
    }
}
