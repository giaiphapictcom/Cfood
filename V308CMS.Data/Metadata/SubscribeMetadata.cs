using System;
using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class SubscribeMetadata
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }
        public int Visister { get; set; }
    }
}