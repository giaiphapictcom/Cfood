using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(OrderTransactionMetadata))]
    [Table("order_transaction")]
    public class OrderTransaction
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string TransactionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public  DateTime? FinishAt { get; set; }
        public byte Status { get; set; }

        [ForeignKey("order_id")]
        public virtual ProductOrder Order { get; set; }
    }
}