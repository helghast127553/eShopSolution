using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Products.Public
{
    public class GetPublicProductPagingRequest: PagingRequestBase
    {
        public int? SubCategoryId { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
