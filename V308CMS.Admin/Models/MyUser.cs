using System;
using System.Collections.Generic;
using V308CMS.Data.Models;


namespace V308CMS.Admin.Models
{
    [Serializable]
    public class MyUser
    {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
        public Data.Admin Admin { get; set; }

        public int RoleId { get; set; }
        public List<Permission> ListPermission { get; set; }

    }
}