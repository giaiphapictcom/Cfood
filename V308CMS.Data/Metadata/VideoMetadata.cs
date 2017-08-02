using System;
using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class VideoMetadata
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        [MaxLength(2500)]
        public string Link { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        public byte Status { get; set; }
        public int TotalView { get; set; }
        public byte Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}