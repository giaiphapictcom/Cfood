using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace V308CMS.Common
{
    public partial class StringClearFormat
    {
        public static string ClearCharacterSpecial(string Name)
        {
            string strValue = string.Empty;
            if (!string.IsNullOrEmpty(Name))
            {
                strValue = Name.ToLower();

                // - Special with char 'A'
                strValue = strValue.Replace("à", "a").Replace("á", "a").Replace("ạ", "a").Replace("ã", "a").Replace("ả", "a");
                strValue = strValue.Replace("ắ", "a").Replace("ằ", "a").Replace("ặ", "a").Replace("ẵ", "a").Replace("ẳ", "a").Replace("ă", "a");
                strValue = strValue.Replace("ấ", "a").Replace("ầ", "a").Replace("ậ", "a").Replace("ẫ", "a").Replace("ẩ", "a").Replace("â", "a");

                // - Special with char 'D'
                strValue = strValue.Replace("đ", "d");

                // - Special with char 'E'
                strValue = strValue.Replace("è", "e").Replace("é", "e").Replace("ẹ", "e").Replace("ẽ", "e").Replace("ẻ", "e");
                strValue = strValue.Replace("ề", "e").Replace("ế", "e").Replace("ệ", "e").Replace("ễ", "e").Replace("ể", "e").Replace("ê", "e");

                // - Special with char 'O'
                strValue = strValue.Replace("ò", "o").Replace("ó", "o").Replace("ọ", "o").Replace("õ", "o").Replace("ỏ", "o");
                strValue = strValue.Replace("ồ", "o").Replace("ố", "o").Replace("ộ", "o").Replace("ỗ", "o").Replace("ổ", "o").Replace("ô", "o");
                strValue = strValue.Replace("ờ", "o").Replace("ớ", "o").Replace("ợ", "o").Replace("ỡ", "o").Replace("ở", "o").Replace("ơ", "o");

                // - Special with char 'U'
                strValue = strValue.Replace("ù", "u").Replace("ú", "u").Replace("ụ", "u").Replace("ũ", "u").Replace("ủ", "u");
                strValue = strValue.Replace("ừ", "u").Replace("ứ", "u").Replace("ự", "u").Replace("ữ", "u").Replace("ử", "u").Replace("ư", "u");

                // - Special with char 'i'
                strValue = strValue.Replace("í", "i").Replace("ì", "i").Replace("ị", "i").Replace("ĩ", "i").Replace("ỉ", "i");

                // - Special with char 'y');
                strValue = strValue.Replace("ỳ", "y").Replace("ý", "y").Replace("ỵ", "y").Replace("ỹ", "y").Replace("ỷ", "i");

                strValue = System.Text.RegularExpressions.Regex.Replace(strValue, "[^0-9a-zA-Z]+?", "-").Replace("--", "-");

            }
            // - Return values
            return strValue.Replace(" ", "-");
        }
        public static string ClearCharacterSpecialOutput(string Name)
        {
            return ClearCharacterSpecial(Name).Replace("-", " ");
        }
        public static string ClearText(string Name)
        {
            string strValue = string.Empty;
            if (!string.IsNullOrEmpty(Name))
            {
                strValue = Name.ToLower();
                strValue = System.Text.RegularExpressions.Regex.Replace(strValue, "[^0-9a-zA-Z]+?", "-").Replace("--", "-");

            }
            // - Return values
            return strValue;
        }

        public static string Clear255(string stringSummary)
        {
            if (!string.IsNullOrEmpty(stringSummary))
            {
                if (stringSummary.Length > 255)
                {
                    stringSummary = stringSummary.Substring(0, 255);
                }
                // Get space max of this string
                for (int i = stringSummary.Length - 1; i >= 0; i--)
                {
                    if (stringSummary[i].CompareTo(' ') == 0)
                    {
                        stringSummary = stringSummary.Substring(0, i);
                        break;
                    }
                }
            }

            return stringSummary;
        }

        public static string Clear100(string stringSummary)
        {
            if (!string.IsNullOrEmpty(stringSummary))
            {
                if (stringSummary.Length > 80)
                {
                    stringSummary = stringSummary.Substring(0, 80);
                }
            }

            return stringSummary;
        }


        public static string StripHtml(string html, bool allowHarmlessTags)
        {
            if (html == null || html == string.Empty)
                return string.Empty;
            //if (allowHarmlessTags)
            html = System.Text.RegularExpressions.Regex.Replace(html, "", string.Empty);
            return System.Text.RegularExpressions.Regex.Replace(html, @"<(.|\n)*?>", string.Empty);
        }

        public static string RemoveHtml(string html)
        {
            if (html == null || html == string.Empty)
                return string.Empty;
            html = Regex.Replace(html, "<title.*?</title>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<object.*?</object>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<w.*?</w.*?>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            html = rx.Replace(html, string.Empty);
            return html;
        }

        public static string GetWebContent(string Address)
        {
            string txOutput = "";
            try
            {
                if (Address != "")
                {
                    WebClient MyClient = new WebClient();
                    Stream MyStream = MyClient.OpenRead(Address);
                    StreamReader MyStreamReader = new StreamReader(MyStream);
                    string NewLine;
                    while ((NewLine = MyStreamReader.ReadLine()) != null)
                    {
                        txOutput += NewLine + "\r\n";
                    }
                }
            }
            catch { }
            return txOutput;
        }
        private static int CountChars(string value)
        {
            int result = 0;
            bool lastWasSpace = false;

            foreach (char c in value)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (lastWasSpace == false)
                    {
                        result++;
                    }
                    lastWasSpace = true;
                }
                else
                {
                    result++;
                    lastWasSpace = false;
                }
            }
            return result;
        }

        public static string LTrim(string s, int Length)
        {
            if (s == null)
                return string.Empty;
            if (s.Length >= Length)
            {
                return s.Substring(0, Length)+"...";
            }
            else
            {
                return s;
            }
        }
    }
}