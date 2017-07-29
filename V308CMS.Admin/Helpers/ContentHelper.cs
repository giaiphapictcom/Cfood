
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

﻿using V308CMS.Data.Enum;


namespace V308CMS.Admin.Helpers
{
    public static class ContentHelper
    {
        public static string ToTitle(this string title, int limit =50, string limitText = "...")
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return "";
            }
            return title.Length > limit ? title.Substring(0, limit) + limitText : title;

        }

        public static string ToPositionName(this byte position)
        {
            switch (position)
            {
                case (byte)BannerPositionEnum.HomeTop:
                    return "Đầu";
                case (byte)BannerPositionEnum.HomeCenter:
                    return "Giữa";
                case (byte)BannerPositionEnum.HomeBottom:
                    return "Dưới";
                default:
                    return "";
            }

        }
    }
}