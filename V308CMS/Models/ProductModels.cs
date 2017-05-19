using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Models
{
    public class ProductModels
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public int SaleOff { get; set; }
    }
}