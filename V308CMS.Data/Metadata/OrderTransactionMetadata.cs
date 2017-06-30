using System;
using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class OrderTransactionMetadata
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        [MaxLength(50)]
        public string TransactionId { get; set; }
      
    }
}