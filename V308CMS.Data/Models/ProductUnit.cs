using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Enum;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [Table("unit")]
    [MetadataType(typeof(UnitMetadata))]
    public class Unit
    {
        public Unit()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            State = (byte)StateEnum.Active;
        }
        public int Id { get; set; }
        public string Name { get; set; }     
        public byte State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}