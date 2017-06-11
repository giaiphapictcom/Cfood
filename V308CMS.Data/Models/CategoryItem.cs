using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data.Models
{
    public class CategoryItem
    {
        public ProductType Root { get; set; }
        public bool IsHasParent { get; set; }
        public List<ProductType> ListParent { get; set; }
        public bool IsHasSub { get; set; }
        public List<ProductType> ListSub { get; set; }
    }
}