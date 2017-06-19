using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V308CMS.Data;

namespace V308CMS.Admin.Models
{
    public class ProductViewModels
    {
        public ProductViewModels()
        {
            Page = 1;
            PageSize = 20;
        }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public int State { get; set; }
        public string Keyword { get; set; }
        public int Page {get;set;}
        public int PageSize {get;set;}
        public int TotalPages { get; set; }
        public List<ProductItem> Data { get; set; }
        public int TotalRecords { get; set; }
        public int Brand { get; set; }
        public int Provider { get; set; }
        public int Manufact { get; set; }
    }
}