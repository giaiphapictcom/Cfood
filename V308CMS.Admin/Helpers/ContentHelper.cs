using V308CMS.Data.Enum;

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

        public static string ToPositionName(this int position)
        {
            switch (position)
            {
                case (int)BannerPositionEnum.HomeTop:
                    return "Đầu";
                case (int)BannerPositionEnum.HomeCenter:
                    return "Giữa";
                case (int)BannerPositionEnum.HomeBottom:
                    return "Dưới";
                default:
                    return "";
            }

        }
    }
}