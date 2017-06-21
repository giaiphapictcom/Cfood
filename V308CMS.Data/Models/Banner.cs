using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(BannerMetadata))]
    [Table("banner")]
   
    public class Banner
    {
        public Banner()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Status = true;

        }
        public int Id { get; set; }
        public string Site { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
       
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Order { get; set; }

        public byte Site { get; set; }

      
    }
}