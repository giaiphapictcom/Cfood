using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(SubscribeMetadata))]
    [Table("subscribe")]
    public class Subscribe
    {
        public Subscribe()
        {
            Created = DateTime.Now;
            Status = true;

            var VisisterRepo = new VisisterRepository();
            Visister = VisisterRepo.CurrentVisisterId();
            

        }
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }
        public int Visister { get; set; }
    }
}