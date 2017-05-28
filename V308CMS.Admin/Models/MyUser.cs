using System;

namespace V308CMS.Admin.Models
{
    [Serializable]
    public class MyUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
        public Data.Admin Admin { get; set; }
    }
}