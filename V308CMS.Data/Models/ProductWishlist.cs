using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [Table("product_wishlist")]
    [MetadataType(typeof(ProductWishlistMetadata))]
    public class ProductWishlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ListProduct { get; set; }
    }
}