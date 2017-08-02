using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class ProductWishlistMetadata
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [MaxLength(2500)]
        public string ListProduct { get; set; }
    }
}