using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public class BoxContentItem
    {
        public ProductType SubCategory { get; set; }
        public List<Product> Products { get; set; }
        public List<Brand> Brands { get; set; }
      
    }
}