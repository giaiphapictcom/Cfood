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

        public static string ToPositionName(this byte position)
        {
            switch (position)
            {
                case (byte)PositionEnum.Top:
                    return "Đầu";
                case (byte)PositionEnum.Center:
                    return "Giữa";
                case (byte)PositionEnum.Bottom:
                    return "Dưới";
                default:
                    return "";
            }
        }
    }
}