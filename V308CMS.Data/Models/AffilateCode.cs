using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using V308CMS.Data.Enum;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(AffilateCodeMetadata))]
    [Table("affilate_code")]
    public class AffilateCode
    {
        public AffilateCode()
        {
            CreatedAt = DateTime.Now;
            Status = (byte)StateEnum.Disable;
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}