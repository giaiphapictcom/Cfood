using System.Collections.Generic;

namespace V308CMS.Data
{
    public class BoxContent
    {
        public BoxContent()
        {
            ListContentItem = new List<BoxContentItem>();
            ListSubCategory = new List<ProductType>();
        }
        public ProductType Category { get; set; }
        public List<ProductType> ListSubCategory { get; set; }
        public List<BoxContentItem> ListContentItem { get; set;}       
       
    }
}