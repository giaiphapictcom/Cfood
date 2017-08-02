using System.ComponentModel.DataAnnotations;
using System;

namespace V308CMS.Data.Metadata
{
    public class RevenueGainMetadata
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int product_id { get; set; }
        public float value { get; set; }
        public byte status { get; set; }
        [Required]
        public int created_by { get; set; }
        public DateTime created { get; set; }
    }
}