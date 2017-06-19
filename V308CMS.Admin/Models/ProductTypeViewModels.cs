using System.Collections.Generic;
using V308CMS.Data;

namespace V308CMS.Admin.Models
{
    public class ProductTypeViewModels
    {
        public int RootId { get; set; }
        public int ParentId { get; set; }
        public int ChildId { get; set; }
        public List<ProductType> Data { get; set; }
    }
}