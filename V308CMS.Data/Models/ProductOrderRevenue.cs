using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(ProductOrderRevenueMetadata))]
    [Table("productorder_revenue")]
    public class ProductOrderRevenue
    {
        public ProductOrderRevenue()
        {
            Created = DateTime.Now;
            Status = 1;

        }
        public int id { get; set; }
        public int OrderNo { get; set; }
        public int Affiliate { get; set; }
        public float Amount { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
    }
}