using V308CMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Common;
using V308CMS.Data.Helpers;

namespace V308CMS.Admin.Helpers
{
    public class AffiliateHelper
    {
        public static int LinkCount() {
            int val = 0;
            
                return val;
        }

        public static string AccountInfo(int uid=0) {
            using (var entities = new V308CMSEntities())
            {
                var user = entities.Account.Where(u=>u.ID == uid).FirstOrDefault();
                if (user != null) {
                    return user.FullName;
                }
            }
            return uid.ToString();
        }
    }
}