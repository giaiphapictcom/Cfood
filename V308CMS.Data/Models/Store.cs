using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Enum;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [Table("store")]
    [MetadataType(typeof(StoreMetadata))]
    public  class Store
    {
        public Store()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            State = (byte)StateEnum.Active;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Manager { get; set; }      
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public byte State { get; set; }


    }
}