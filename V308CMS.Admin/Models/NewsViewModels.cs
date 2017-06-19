using System.Collections.Generic;
using V308CMS.Data;

namespace V308CMS.Admin.Models
{
    public class NewsViewModels
    {
        public int CategoryId { get; set; }
        public int Site { get; set; }
        public List<News> Data { get; set; }
    }
}