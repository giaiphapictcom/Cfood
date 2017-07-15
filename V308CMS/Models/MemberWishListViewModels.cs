using System.Collections.Generic;
using V308CMS.Data;

namespace V308CMS.Models
{
    public class MemberWishListViewModels
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }
        public List<Product> ListProduct { get; set; }

    }
}