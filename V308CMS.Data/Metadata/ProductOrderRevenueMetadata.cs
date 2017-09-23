using System;
using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class ProductOrderRevenueMetadata
    {
        [Key]
        public int id { get; set; }
       //s public int OrderNo { get; set; }
        public int Affiliate { get; set; }
        public float Amount { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
    }
}