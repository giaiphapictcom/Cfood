using System;
using System.Collections.Generic;

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
        public  KeyValuePair<string, int> Affilate { get; set; }
    }
}