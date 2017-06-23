using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [Table("menu_config")]
    [MetadataType(typeof(MenuConfigMetadata))]
    public class MenuConfig
    {
        public MenuConfig()
        {
            CreatedAt = DateTime.Now;
            
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Link { get; set; }
        public string Target { get; set; }
        
        public byte State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Site { get; set; }
        public int Order { get;set;}
       

    }
}