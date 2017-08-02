using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(RegionMetadata))]
    [Table("region")]
    public class Region
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ParentId { get; set; }
    }
}