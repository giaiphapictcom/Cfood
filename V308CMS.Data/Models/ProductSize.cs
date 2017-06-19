using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(ProductSizeMetadata))]
    [Table("product_size")]
    public class ProductSize
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        public string Size { get; set; }
        
        [ForeignKey("ProductId")]
        [Required]
        public virtual Product Product { get; set; }
    }
}