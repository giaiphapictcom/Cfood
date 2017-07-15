using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Enum;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [Table("voucher")]
    [MetadataType(typeof(VoucherMetadata))]
    public class Voucher
    {
        public Voucher()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            State = (byte) StateEnum.Active;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public  DateTime? ExpireDate { get; set; }
        public byte DiscountType { get; set; }  
        public double Value { get; set; }      
        public byte State { get; set; }
        public  DateTime StartDate { get; set; }        
    }
}