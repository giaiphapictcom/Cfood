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

        private string _Target;
        public string Target { get { if (String.IsNullOrEmpty(_Target)) return ""; else return _Target; } set { _Target = value; } }

        private string _Site;
        public string Site { get { if (String.IsNullOrEmpty(_Site)) return "home"; else return _Site; } set { _Site = value; } }

        public byte State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Order { get;set;}
       

    }
}