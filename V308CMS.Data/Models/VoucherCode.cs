using System;
using V308CMS.Data.Enum;

namespace V308CMS.Data.Models
{
    public class VoucherCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int VoucherId { get; set; }
        public int UserId { get; set; }
        public byte State { get; set; }
        public DateTime CreatedAt { get; set; }

        public VoucherCode()
        {
            State =(byte)StateEnum.Active;
            CreatedAt = DateTime.Now;
        }
    }
}