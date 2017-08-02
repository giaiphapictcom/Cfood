using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [Table("shipping_address")]
    [MetadataType(typeof(ShippingAddress))]
    public class ShippingAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IpAddress { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Ward { get; set; }
        public string Address { get; set; }
        public  bool Default { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }
}