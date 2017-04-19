using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace V308CMS.Common
{
    public class V308Mail
    {

        public V308Mail()
        { }

        public static string SendMail(string to, string subject, string body)
        {
            string SendersAddress = "hoangv308@gmail.com";
            string ReceiversAddress = to;
            string SendersPassword = "duyenyen06011990";
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(SendersAddress, SendersPassword),
                };
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(SendersAddress, ReceiversAddress, subject, body) { IsBodyHtml = true };
                //message.To.Add("thuyhang@365bay.vn");
                //message.To.Add("hoangv308@gmail.com");
                smtp.Send(message);
                return "OK";
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                throw;
            }
        }

    }
    public class Ultility
    {
        static public string convertValueToDistrict(int pValue)
        {

            string str = "";
            switch (pValue)
            {
                case 1: str = "Huyện Ba Vì";
                    break;
                case 2: str = "Huyện Chương Mỹ";
                    break;
                case 3: str = "Huyện Đan Phượng";
                    break;
                case 4: str = "Huyện Đông Anh";
                    break;
                case 5: str = "Huyện Gia Lâm";
                    break;
                case 6: str = "Huyện Hoài Đức";
                    break;
                case 7: str = "Huyện Mê Linh";
                    break;
                case 8: str = "Huyện Mỹ Đức";
                    break;
                case 9: str = "Huyện Phú Xuyên";
                    break;
                case 10: str = "Huyện Phúc Thọ";
                    break;
                case 11: str = "Huyện Quốc Oai";
                    break;
                case 12: str = "Huyện Sóc Sơn";
                    break;
                case 13: str = "Huyện Thạch Thất";
                    break;
                case 14: str = "Huyện Thanh Oai";
                    break;
                case 15: str = "Huyện Thanh Trì";
                    break;
                case 16: str = "Huyện Thường Tín";
                    break;
                case 17: str = "Huyện Từ Liêm";
                    break;
                case 18: str = "Huyện Ứng Hòa";
                    break;
                case 19: str = "Quận Ba Đình";
                    break;
                case 20: str = "Quận Cầu Giấy";
                    break;
                case 21: str = "Quận Đống Đa";
                    break;
                case 22: str = "Quận Hai Bà Trưng";
                    break;
                case 23: str = "Quận Hoàn Kiếm";
                    break;
                case 24: str = "Quận Hoàng Mai";
                    break;
                case 25: str = "Quận Long Biên";
                    break;
                case 26: str = "Quận Tây Hồ";
                    break;
                case 27: str = "Quận Thanh Xuân";
                    break;
                case 28: str = "Quận Hà Đông";
                    break;
                case 29: str = "Thị xã Sơn Tây";
                    break;
                default: 
                    str = "Hà Nội";
                    break;
            }
            return str;
        }
        static public string SubtractStringTitle(string pValue)
        {

            string[] Split = pValue.Split(new Char[] { ' ' });
            string str = "";
            if (Split.Count() > 7)
            {
                str = (Split[0] + " " + Split[1] + " " + Split[2] + " " + Split[3] + " " + Split[4] + " " + Split[5] + " " + Split[6] + " ...");
                //if (str.Length > 33)
                //{
                //    return str.Remove(33) + " ...";
                //}
                //else
                //{
                //    return str;
                //}
                return str;
            }
            //else if (pValue.Length > 33)
            //{
            //    return pValue.Remove(33) + " ...";
            //}
            else
            {
                return pValue;
            }

        }
        static public string SubtractStringTitle2(string pValue)
        {

            string[] Split = pValue.Split(new Char[] { ' ' });
            string str = "";
            if (Split.Count() > 7)
            {
                str = (Split[0] + " " + Split[1] + " " + Split[2] + " " + Split[3] + " " + Split[4] + " " + Split[5] + " " + Split[6] + " ...");
                //if (str.Length > 36)
                //{
                //    return str.Remove(36) + " ...";
                //}
                //else
                //{
                //    return str;
                //}
                return str;
            }
            //else if (pValue.Length > 36)
            //{
            //    return pValue.Remove(36) + " ...";
            //}
            else
            {
                return pValue;
            }

        }
        static public string SubtractStringSummary(string pValue)
        {

            string[] Split = pValue.Split(new Char[] { ' ' });
            string str = "";
            if (Split.Count() > 40)
            {
                for (int i = 0; i < 40; i++)
                {
                    str += Split[i].ToString() + " ";
                }
                str += " ...";
                return str;
            }
            else
            {
                return pValue;
            }

        }
        static public string SubtractStringSummaryWithNumber(string pValue, int pNumber)
        {

            string[] Split = pValue.Split(new Char[] { ' ' });
            string str = "";
            if (Split.Count() > pNumber)
            {
                for (int i = 0; i < pNumber; i++)
                {
                    str += Split[i].ToString() + " ";
                }
                str += " ...";
                return str;
            }
            else
            {
                return pValue;
            }

        }
        static public string CheckMenu(int? pValue1, int? pValue2)
        {
            string mStr = "";
            try
            {
                if (pValue1 == pValue2)
                {
                    mStr = "class=\"active\"";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                return "";
            }
            return mStr;
        }
        static public string CheckSeclectSelected(int? pValue1, int? pValue2)
        {
            string mStr = "";
            try
            {
                if (pValue1 == pValue2)
                {
                    mStr = "selected=\"selected\"";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                return "";
            }
            return mStr;
        }
        static public string CheckBoxchecked(bool? pValue)
        {
            string mStr = "";
            try
            {
                if (pValue == true)
                {
                    mStr = "checked=\"checked\"";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                return "";
            }
            return mStr;
        }
        static public string CheckSubMenu(int? pValue1, int? pValue2)
        {
            string mStr = "";
            try
            {
                if (pValue1 == pValue2)
                {
                    mStr = "style=\"display: block;\"";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                return "";
            }
            return mStr;
        }
        static public string LocDau(string pValue)
        {
            try
            {

                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string strRuler = pValue.ToLower().Normalize(System.Text.NormalizationForm.FormD);
                strRuler = regex.Replace(strRuler, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                return Regex.Replace(strRuler, @"[^\w\.-]", "-");
            }
            catch
            {
                return "a-a";
            }
        }

        static public string URITitle(string str)
        {
            try
            {

                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string strRuler = str.ToLower().Normalize(System.Text.NormalizationForm.FormD);
                strRuler = regex.Replace(strRuler, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                return Regex.Replace(strRuler, @"[^\w\.-]", "-");
            }
            catch
            {
                return "a-a";
            }
        }

        static public string SiteUrl(string val) {
            string url = "";
            return url;
        }

        static public string ImgUrl(string val)
        {
            
            string url = "//" + HttpContext.Current.Request.Url.Host + (HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":" + HttpContext.Current.Request.Url.Port);
            return url + '/' + val;
        }

        static public string LocDau2(string pValue)
        {
            try
            {

                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string strRuler = pValue.ToLower().Normalize(System.Text.NormalizationForm.FormD);
                strRuler = regex.Replace(strRuler, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                return Regex.Replace(strRuler, @"[^\w\.-]", "");
            }
            catch
            {
                return "a-a";
            }
        }
    }
}