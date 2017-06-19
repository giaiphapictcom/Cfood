using System;

namespace V308CMS.Data
{
    public class ProductItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }
        public int? Quantity { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public double? Price { get; set; }
        public double? Npp { get; set; }
        public int Order { get; set; }

    }
}