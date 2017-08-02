using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace V308CMS.Common
{
    public static class StringHelper
    {
        public static string ToRemoveWhitespace(this string str)
        {
            str = str.Replace(" ", "");
            while (str.Contains("  "))
            {
                str = str.Replace(" ", "");
            }
            return str;
        }
        public static string ToUnsign(this string str)
        {
            return Ultility.LocDau2(str);
        }
        public static string ToSlug(this string title)
        {
            return Ultility.URITitle(title);
        }
        public static string TruncateAtWord(string word, int limit = 10)
        {
            word = word.Substring(0, Math.Min(limit, word.Length));
            return word;
        }
        private const string NumberChars = "0123456789";
        private const string RandChars = "0123456789qwertyuiopasdfghjklzxcvbnmERTYUIOPASDFGHJKLZXCVBNM";
        private const string AlphaUpperCase = "ABCDEFGHIKLNOPQXYZTVRUS";
        public static string GenerateUpperCasePrefix(int length =1)
        {
            var random = new Random();
            var captcha = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                captcha.Append(AlphaUpperCase[random.Next(AlphaUpperCase.Length)]);
            }
            return captcha.ToString();
        }

        public static string GenerateNumber(int length)
        {
            var random = new Random();
            var captcha = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                captcha.Append(NumberChars[random.Next(NumberChars.Length)]);
            }
            return captcha.ToString();
        }
        public static string GenerateString(int length)
        {
            var random = new Random();
            var captcha = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                captcha.Append(RandChars[random.Next(RandChars.Length)]);
            }
            return captcha.ToString();
        }

     
        public static string ToFirstWords(this string str, int numberWords = 10)
        {
            int words = numberWords;
            var result = string.Empty;
            // Loop through entire summary.
            for (int i = 0; i < str.Length; i++)
            {
                // Increment words on a space.
                if (str[i] == ' ')
                {
                    words--;
                }
                // If we have no more words to display, return the substring.
                if (words == 0)
                {
                    result = str.Substring(0, i);
                    break;
                }
            }
            return result;

        }
        private static String Dec2Hex(int textString)
        {
            int t = textString + 0;
            var temp = "&#" + textString.ToString().ToUpper() + ";";
            return temp.Trim();
        }

        public static string ToDecimalNcr(this String str)
        {
            var outputString = "";
            int haut = 0;
            foreach (char b in str)
            {
                if (b < 0 || b > 0xFFFF)
                {
                    outputString += "!error " + Dec2Hex(b) + "!";
                }
                if (haut != 0)
                {
                    if (0xDC00 <= b && b <= 0xDFFF)
                    {
                        outputString += Dec2Hex(0x10000 + ((haut - 0xD800) << 10) + (b - 0xDC00)) + "";
                        haut = 0;
                        continue;
                    }
                    else
                    {
                        outputString += "!error " + Dec2Hex(haut) + "!";
                        haut = 0;
                    }
                }
                if (0xD800 <= b && b <= 0xDBFF)
                {
                    haut = b;
                }
                else
                {
                    outputString += Dec2Hex(b) + "";
                }
            }
            return outputString;
        }

        public static string DaynameVN(String name="")
        {
            string daynameVN = "";
            switch (name) {
                case "Monday":
                    daynameVN = "Thứ 2";break;
                case "Tuesday":
                    daynameVN = "Thứ 3"; break;
                case "Wednesday":
                    daynameVN = "Thứ 4"; break;
                case "Thursday":
                    daynameVN = "Thứ 5"; break;
                case "Friday":
                    daynameVN = "Thứ 6"; break;
                case "Saturday":
                    daynameVN = "Thứ 7"; break;
                case "Sunday":
                    daynameVN = "Chủ Nhật"; break;

            }
            return daynameVN;
        }
    }
}