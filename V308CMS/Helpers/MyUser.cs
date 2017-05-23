using System;

namespace V308CMS.Helpers
{
    [Serializable]
    public class MyUser
    {
        public MyUser()
        {
            
        }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
       

    }
}