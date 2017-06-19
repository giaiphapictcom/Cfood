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
        public DateTime DateCreate { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int AccountID { get; set; }
        public int Status { get; set; }
     }
}