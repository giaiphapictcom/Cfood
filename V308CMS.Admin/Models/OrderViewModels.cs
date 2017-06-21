using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V308CMS.Data;

namespace V308CMS.Admin.Models
{
    public class OrderViewModels
    {
        public byte Status { get; set; }
        public byte SearchType { get; set; }
        public string Keyword { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<ProductOrder> Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

    }
}