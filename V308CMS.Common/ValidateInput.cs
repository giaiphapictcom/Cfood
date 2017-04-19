using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace V308CMS.Common
{
    public class ValidateInput
    {
        public static string FormatInput(object stringinput)
        {
            try
            {
                string stringoutput;
                stringoutput = stringinput.ToString().Replace("'", "&#146;");
                stringoutput = stringinput.ToString().Replace("script", "&#115;cript");
                stringoutput = stringinput.ToString().Replace("SCRIPT", "&#083;CRIPT");
                stringoutput = stringinput.ToString().Replace("Script", "&#083;cript");
                stringoutput = stringinput.ToString().Replace("script", "&#083;cript");
                stringoutput = stringinput.ToString().Replace("object", "&#111;bject");
                stringoutput = stringinput.ToString().Replace("OBJECT", "&#079;BJECT");
                stringoutput = stringinput.ToString().Replace("Object", "&#079;bject");
                stringoutput = stringinput.ToString().Replace("object", "&#079;bject");
                stringoutput = stringinput.ToString().Replace("applet", "&#097;pplet");
                stringoutput = stringinput.ToString().Replace("APPLET", "&#065;PPLET");
                stringoutput = stringinput.ToString().Replace("Applet", "&#065;pplet");
                stringoutput = stringinput.ToString().Replace("applet", "&#065;pplet");
                stringoutput = stringinput.ToString().Replace("event", "&#101;vent");
                stringoutput = stringinput.ToString().Replace("EVENT", "&#069;VENT");
                stringoutput = stringinput.ToString().Replace("Event", "&#069;vent");
                stringoutput = stringinput.ToString().Replace("event", "&#069;vent");
                stringoutput = stringinput.ToString().Replace("document", "&#100;ocument");
                stringoutput = stringinput.ToString().Replace("DOCUMENT", "&#068;OCUMENT");
                stringoutput = stringinput.ToString().Replace("Document", "&#068;ocument");
                stringoutput = stringinput.ToString().Replace("document", "&#068;ocument");
                stringoutput = stringinput.ToString().Replace("cookie", "&#099;ookie");
                stringoutput = stringinput.ToString().Replace("COOKIE", "&#067;OOKIE");
                stringoutput = stringinput.ToString().Replace("Cookie", "&#067;ookie");
                stringoutput = stringinput.ToString().Replace("cookie", "&#067;ookie");
                stringoutput = stringinput.ToString().Replace("form", "&#102;orm");
                stringoutput = stringinput.ToString().Replace("FORM", "&#070;ORM");
                stringoutput = stringinput.ToString().Replace("Form", "&#070;orm");
                stringoutput = stringinput.ToString().Replace("form", "&#070;orm");
                stringoutput = stringinput.ToString().Replace("iframe", "i&#102;rame");
                stringoutput = stringinput.ToString().Replace("IFRAME", "I&#070;RAME");
                stringoutput = stringinput.ToString().Replace("Iframe", "I&#102;rame");
                stringoutput = stringinput.ToString().Replace("iframe", "i&#102;rame");
                stringoutput = stringinput.ToString().Replace("textarea", "&#116;extarea");
                stringoutput = stringinput.ToString().Replace("TEXTAREA", "&#84;EXTAREA");
                stringoutput = stringinput.ToString().Replace("Textarea", "&#84;extarea");
                stringoutput = stringinput.ToString().Replace("textarea", "&#84;extarea");
                stringoutput = stringinput.ToString().Replace("input", "&#073;nput");
                stringoutput = stringinput.ToString().Replace("Input", "&#073;nput");
                stringoutput = stringinput.ToString().Replace("INPUT", "&#073;nput");
                stringoutput = stringinput.ToString().Replace("input", "&#073;nput");
                stringoutput = stringinput.ToString().Replace("<", "&lt;");
                stringoutput = stringinput.ToString().Replace(">", "&gt;");
                stringoutput = stringinput.ToString().Replace("'", "''");
                stringoutput = stringinput.ToString().Replace("=", "&#061;");
                stringoutput = stringinput.ToString().Replace("select", "sel&#101;ct");
                stringoutput = stringinput.ToString().Replace("join", "jo&#105;n");
                stringoutput = stringinput.ToString().Replace("union", "un&#105;on");
                stringoutput = stringinput.ToString().Replace("where", "wh&#101;re");
                stringoutput = stringinput.ToString().Replace("insert", "ins&#101;rt");
                stringoutput = stringinput.ToString().Replace("delete", "del&#101;te");
                stringoutput = stringinput.ToString().Replace("update", "up&#100;ate");
                stringoutput = stringinput.ToString().Replace("like", "lik&#101;");
                stringoutput = stringinput.ToString().Replace("drop", "dro&#112;");
                stringoutput = stringinput.ToString().Replace("create", "cr&#101;ate");
                stringoutput = stringinput.ToString().Replace("modify", "mod&#105;fy");
                stringoutput = stringinput.ToString().Replace("rename", "ren&#097;me");
                stringoutput = stringinput.ToString().Replace("alter", "alt&#101;r");
                stringoutput = stringinput.ToString().Replace("cast", "ca&#115;t");


                return stringoutput;
            }
            catch
            {

                return string.Empty;
            }
        }
        public static bool CheckInputUserName(string userName)
        {
            try
            {
                var art = new List<string>
	                            {
		                            "0","1","2","3","4","5","6","7","8","9","-",".",
		                            "a","b","c","d","e","f","g","h","i","j","l","k","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
	                            };
                var strchar2 = new List<string>
	                            {
		                            "0","1","2","3","4","5","6","7","8","9",
		                            "a","b","c","d","e","f","g","h","i","j","l","k","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
	                            };
                var strchar = new List<string>
	                            {
		                            "a","b","c","d","e","f","g","h","i","j","l","k","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
	                            };

                if (userName.Length > 255 || userName.Length < 8)
                    return false;

                string first = userName.Substring(0, 1);

                int length = IsInt(userName.Length);
                int fLength = length - 1;
                string last = userName.Substring(fLength);

                var item = strchar.Where(c => c.Equals(first.ToLower())).FirstOrDefault();

                if (item == null || item == string.Empty)
                {
                    return false;
                }

                var item1 = strchar2.Where(c => c.Equals(last.ToLower())).FirstOrDefault();

                if (item1 == null || item1 == string.Empty)
                {
                    return false;
                }
                foreach (char a in userName.ToLower().ToCharArray())
                {
                    var substring = art.Where(c => c.Equals(a.ToString().ToLower())).FirstOrDefault();
                    if (string.IsNullOrEmpty(substring))
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsValidEmailAddress(string EmailAddress)
        {
            Regex regEmail = new Regex(@"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (regEmail.IsMatch(EmailAddress))
                return true;

            return false;
        }
        public static string ConvertTimeToSecons(double totalSecons)
        {
            try
            {
                TimeSpan _time = TimeSpan.FromSeconds(totalSecons);
                int h = 0;
                int m = 0;
                int s = 0;
                int d = 0;
                string hh = string.Empty;
                string mm = string.Empty;
                string ss = string.Empty;
                string dd = string.Empty;
                d = ValidateInput.IsInt(totalSecons) / 86400;
                h = ((ValidateInput.IsInt(totalSecons) - d * 86400) / 3600) + d * 24;
                m = (ValidateInput.IsInt(totalSecons) - h * 3600) / 60;
                s = ValidateInput.IsInt(totalSecons) - h * 3600 - m * 60;

                if (h < 10)
                {
                    hh = "0" + h.ToString();
                }
                else
                {
                    hh = h.ToString();
                }
                if (m < 10)
                {
                    mm = "0" + m.ToString();
                }
                else
                {
                    mm = m.ToString();
                }
                if (s < 10)
                {
                    ss = "0" + s.ToString();
                }
                else
                {
                    ss = s.ToString();
                }
                if (d < 10)
                {
                    dd = "0" + d.ToString();
                }
                else
                {
                    dd = d.ToString();
                }

                return String.Format("{0}h:{1}m:{2}s", hh, mm, ss);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static short IsShort(object inputText)
        {
            try
            {
                return short.Parse(inputText.ToString());
            }
            catch
            {
                return 0;
            } 
       }
        public static TimeSpan IsTimeSpan(object inputText)
        {
            try
            {
                return TimeSpan.Parse(inputText.ToString());
            }
            catch
            {
                return TimeSpan.FromSeconds(0);
            }
        }
        public static float IsFloat(object inputText)
        {
            try
            {
                return float.Parse(inputText.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static double IsDouble(object inputText)
        {
            try
            {
                return Convert.ToDouble(inputText);
            }
            catch
            {
                return 0;
            }
        }
        public static byte IsByte(object inputText)
        {
            try
            {
                return Convert.ToByte(inputText);
            }
            catch 
            {
                return 0;
            }
        }
        public static int GoldFastBuild(int level)
        {
            try
            {
                int i = level / 5;

                return (AppSetting.GoldFastBuild + i * AppSetting.GoldFastBuild);
            }
            catch
            {
                return AppSetting.GoldFastBuild;
            }
        }
        public static bool IsBool(object inputText)
        {
            try
            {
                return Convert.ToBoolean(inputText);
            }
            catch
            {
                return false;
            }
        }
        public static DateTime IsDateTime(object isdate)
        {
            try
            {
                return Convert.ToDateTime(isdate);
            }
            catch
            {
                return DateTime.Now;
            }
        }
        public static int IsInt(object inputText)
        {
            try
            {
                return Convert.ToInt32(inputText);
            }
            catch
            {
                return 0;
            }
        }
        public static long IsLong(object inputText)
        {
            try
            {
                return Convert.ToInt64(inputText);
            }
            catch
            {
                return 0;
            }
        }
        public static bool IsImage(string fileName)
        {
            if (fileName != null
                && (fileName.IndexOf(".jpg") > -1 || fileName.IndexOf(".gif") > -1 || fileName.IndexOf(".png") > -1)
                && (fileName.Length > 4 && fileName.Length < 200))
            {
                return true;
            }
            return false;
        }
        public static decimal IsDecimal(object obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return 0;
            }
        }
        public static bool IsFlash(string fileName)
        {
            if (fileName != null && fileName.IndexOf(".swf") > -1)
            {
                return true;
            }
            return false;
        }
        public static string InputText(string inputText)
        {
            return inputText;
        }
        public static string ClearInputText(string inputText)
        {
            if (!string.IsNullOrEmpty(inputText))
            {
                inputText = System.Text.RegularExpressions.Regex.Replace(inputText, "[^0-9a-zA-Z]+?", "");
            }

            return inputText;
        }
        public static bool SetCookieUser(string _value, bool createdPesistent)
        {
            try
            {
                HttpCookie cookie = new HttpCookie("DeCheId");
                if (createdPesistent)
                    cookie.Expires = DateTime.Now.AddDays(360);
                cookie.Value = _value;;

                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string GetCookieUser()
        {
            try
            {
                if (HttpContext.Current.Request.Cookies["DeCheId"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["DeCheId"].Value))
                {
                    return HttpContext.Current.Request.Cookies["DeCheId"].Value;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }
        public static bool CheckLoginUser()
        {
            try
            {
                if (HttpContext.Current.Request.Cookies["DeCheId"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["DeCheId"].Value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }
        public static bool LogOut()
        {
            try
            {
                SetCookieUser(string.Empty, false);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}