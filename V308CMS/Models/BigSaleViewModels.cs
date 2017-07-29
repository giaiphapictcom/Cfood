using System.Collections.Generic;
using System.Web.Mvc;
using V308CMS.Data;

namespace V308CMS.Models
{
    public class BigSaleViewModels
    {
        public int SaleOff { get; set; }
     
        public int Sort { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }
      
        public List<Product> ListProduct { get; set; }
        public List<Product> ListProductBestSeller { get; set; }
        public List<SelectListItem> ListSort { get; set; }
        public string Filter { get; set; }
    }
}