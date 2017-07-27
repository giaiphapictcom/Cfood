using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(AffilateUserMetadata))]
    [Table("affilate_user")]
    public class AffilateUser
    {
        public AffilateUser()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AffilateId { get; set; }
        public int Amount { get; set; }
        public  DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public byte Status { get; set; }


        [ForeignKey("UserId")]
        public virtual Account User { get; set; }

    }
}