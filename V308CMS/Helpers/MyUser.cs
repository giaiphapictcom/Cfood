using System;

namespace V308CMS.Helpers
{
    [Serializable]
    public class MyUser
    {
        public MyUser()
        {
            
        }
        public int UserId { get; set; }
        public string UserName { get; set; }     
        public string Avatar { get; set; }
    }
}