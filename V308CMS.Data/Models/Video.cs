using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [Table("video")]
    [MetadataType(typeof(VideoMetadata))]
    public class Video
    {
        public Video()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;

        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Code { get; set; }
        public byte Status { get; set;}
        public int TotalView { get; set; }
        public byte Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Order { get; set; }

    }
}