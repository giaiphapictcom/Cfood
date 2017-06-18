using System.Collections.Generic;
using V308CMS.Data;

namespace V308CMS.Admin.Models
{
    public class UserViewModels
    {
        public UserViewModels()
        {
            Page = 1;
            PageSize = 15;
        }
        public int State { get; set; }
        public string Keyword { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<Account> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}