﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Products.Manage
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }

        public string Name { set; get; }

        public string Description { set; get; }

        public string Details { set; get; }

        public string SeoDescription { set; get; }

        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }

        public string LanguageId { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }
}
