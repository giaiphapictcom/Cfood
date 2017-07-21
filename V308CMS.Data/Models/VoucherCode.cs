using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Enum;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [Table("voucher_code")]
    [MetadataType(typeof(VoucherCodeMetadata))]
    public class VoucherCode
    {
        public VoucherCode()
        {
            State = (byte)StateEnum.Active;
            CreatedAt = DateTime.Now;
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public int VoucherId { get; set; }      
        public byte State { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("VoucherId")]
        public virtual Voucher Voucher { get; set; }

     

    }
}