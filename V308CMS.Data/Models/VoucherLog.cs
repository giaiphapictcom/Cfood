using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace V308CMS.Data.Models
{
    public class VoucherLog
    {
        public VoucherLog()
        {
            CreatedAt = DateTime.Now;
        }

        public int  Id { get; set; }
        public int UserId { get; set; }
        public int VoucherId { get; set; }
        public string VoucherCode { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("VoucherId")]
        public virtual Voucher Voucher { get; set; }

        [ForeignKey("UserId")]
        public virtual Account User { get; set; }


    }
}