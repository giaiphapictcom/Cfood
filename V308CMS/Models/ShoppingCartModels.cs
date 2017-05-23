using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Models
{
    public class ShoppingCartModels
    {
        public int item_count { get; set; }
        public List<ProductsCartModels> items { get; set; }
        public double total_price { get; set; }
     }
}