using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Enum;
using V308CMS.Data.Metadata;


namespace V308CMS.Data.Models
{
    [Table("product_revenue_gain")]
    [MetadataType(typeof(RevenueGainMetadata))]
    public class RevenueGain
    {
        public RevenueGain()
        {
            status = (byte)StateEnum.Active;
            created = DateTime.Now;
            value = 0;
        }
        public int id { get; set; }
        public int product_id { get; set; }
        public double value { get; set; }
        public int status { get; set; }
        public int created_by { get; set; }
        public DateTime created { get; set; }

        //public int VoucherId { get; set; }
        //[ForeignKey("VoucherId")]
        //public virtual Voucher Voucher { get; set; }

        
        //[ForeignKey("UserId")]
        //public virtual Account User { get; set; }
    }
}