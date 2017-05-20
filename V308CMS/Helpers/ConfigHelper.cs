using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V308CMS.Common;

namespace V308CMS.Helpers
{
    public static class ConfigHelper
    {
        public static string GmailSmtpServer
        {
            get { return Configs.GetItemConfig("host"); }
        }
        public static string GMailUserName {
            get { return Configs.GetItemConfig("userName"); }
        }

        public static string GMailPassword
        {
            get { return Configs.GetItemConfig("password"); }
        }
        public static string GMailPort
        {
            get { return Configs.GetItemConfig("port"); }
        }

        public static string Domain
        {
            get { return Configs.GetItemConfig("Doamin"); }
        }
    }
}