using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}