using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class ProductSizeMetadata
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Size { get; set; }

        public int ProductId { get; set; }

    }
}