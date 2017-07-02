using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class ShippingAddressMetadata
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(250)]
        public string Region { get; set; }
        [MaxLength(250)]
        public string City { get; set; }
        [MaxLength(250)]
        public string Ward { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        public bool Default { get; set; }
    }
}