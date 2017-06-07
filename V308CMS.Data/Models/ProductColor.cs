using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(ProductColorMetadata))]
    [Table("product_color")]
    public class ProductColor
    {
        public int Id { get; set; }
       
        public int ProductId { get; set; }
        public string ColorCode { get; set; }

        [ForeignKey("ProductId")]
        [Required]
        public virtual Product Product { get; set; }
    }
}