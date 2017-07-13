using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V308CMS.Data;

namespace V308CMS.Models
{
    public class SearchViewModels
    {
        public string Keyword { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public List<Product> ListProduct { get; set; }


    }
}