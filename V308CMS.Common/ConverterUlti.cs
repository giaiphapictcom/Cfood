using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Common
{
    public class ConverterUlti
    {

        static public string Convert2String(string a, string b)
        {


            try
            {
                int x = Int32.Parse(a);
                int y = Int32.Parse(b);
                int z = (x + y);
                if (z > 150)
                {
                    z = 150;
                }
                else if (z < (-150))
                {
                    z = (-150);
                }
                return (z).ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                return "0";
            }




        }
        static public bool ConvertStringToLogic(string val)
        {
            bool re = false;
            if (string.IsNullOrEmpty(val))
            {
                re = false;
                return re;
            }

            else if (val.Equals("on"))
                re = true;
            else if (val.Equals("off"))
                re = false;

            return re;

        }
        static public string ConvertLogicToString(bool? val)
        {
            string re = "";
            if (val == null)
            {
                re = "";
                return re;
            }

            else if (val == true)
                re = "checked=\"checked\"";
            else if (val == false)
                re = "";

            return re;

        }
        static public bool? ConvertIntToLogic(int? val)
        {
            bool re = false;
            if (val != null)
            {
                if (val == 1)
                    re = true;
            }
            return re;

        }
        static public bool? ConvertStringToLogic2(string val)
        {
            bool re = false;
            if (!String.IsNullOrEmpty(val))
            {
                if (val.Trim().Equals("checked"))
                    re = true;
            }
            return re;

        }
        static public string ConvertGenderLogicToString(bool? val)
        {
            string re = "";
            if (val == null)
                re = "Không Rõ";
            else if (val == true)
                re = "Nam";
            else if (val == false)
                re = "Nữ";
            else
                re = "Không Rõ";
            return re;


        }
        static public string ConverUnitWeight(int? val)
        {
            string re = "";
            if (val == 0)
                re = "Kg";
            else if (val == 1)
                re = "Kg";
            else if (val == 2)
                re = "Đĩa";
            else if (val == 3)
                re = "Mớ";
            else if (val == 4)
                re = "Gói";
            else if (val == 5)
                re = "Túi";
            else if (val == 6)
                re = "Hộp";
            else if (val == 7)
                re = "Cái";
            else if (val == 8)
                re = "Món";
            else if (val == 9)
                re = "Nồi";
            else if (val == 10)
                re = "Xuất";
            else if (val == 11)
                re = "Nữ";
            else
                re = "Tô";
            return re;


        }
        static public string ConvertGenderLogicToString2(string val)
        {
            string re = "";
            if (string.IsNullOrEmpty(val))
                re = "Không Rõ";
            else if (val.Equals("1"))
                re = "Nam";
            else if (val.Equals("0"))
                re = "Nữ";
            else
                re = "Không Rõ";
            return re;


        }
        static public string GetNgayDang(string date)
        {
            string str = "";
            try
            {
                DateTime dt = DateTime.Parse(date);
                str = dt.ToString("dd/MM/yyyy hh:mm:ss");
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "Không Rõ";
            }
            return str;

        }
        static public string GetNgayDangByDateTime(DateTime date)
        {
            string str = "";
            try
            {
                DateTime dt = date;
                str = dt.ToString("dd/MM/yyyy hh:mm:ss");
            }
            catch (Exception ex)
            {
                return "Không Rõ";
            }
            return str;

        }
        static public string GetIdProductByDateTime(DateTime date)
        {
            string str = "";
            try
            {
                DateTime dt = date;
                str = dt.ToString("dd/MM/yyyy").Replace("/","");
            }
            catch (Exception ex)
            {
                return "Không Rõ";
            }
            return str;

        }
        static public string GetNgayDangChat(string date)
        {
            string str = "";
            try
            {
                DateTime dt = DateTime.Parse(date);
                str = dt.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                return "Không Rõ";
            }
            return str;

        }
        static public string GetNgayDangReport(string date)
        {
            string str = "";
            try
            {
                DateTime dt = DateTime.Parse(date);
                int day = DateTime.Now.Day;
                int mon = DateTime.Now.Month;
                if (dt.Day == day && dt.Month == mon)
                {
                    return str = "Hôm nay " + dt.ToString("HH:mm");
                }
                else
                {
                    if (day == 1)
                    {
                        int year = DateTime.Now.Year;
                        if (mon == 1)
                        {
                            mon = 12;
                            year -= 1;
                        }
                        else { mon -= 1; }
                        day = DateTime.DaysInMonth(mon, year);

                    }
                    else { day -= 1; }
                    if (dt.Day == day && dt.Month == mon)
                    {
                        return str = "Hôm qua " + dt.ToString("HH:mm");
                    }
                }
                str = dt.ToString("dd/MM/yy HH:mm");
            }
            catch (Exception ex)
            {
                return "Không Rõ";
            }
            return str;

        }
        static public DateTime ConvertStringToDateTime(string myTime)
        {
            DateTime myDate;
            try
            {
                myDate = DateTime.Parse(myTime);
                return myDate;
            }
            catch (Exception ex)
            {
                return new DateTime();
            }

        }
        static public string ConvertPortal(string date)
        {
            string str = "";
            try
            {
                DateTime dt = DateTime.Parse(date);
                int day = DateTime.Now.Day;
                int mon = DateTime.Now.Month;
                str = dt.ToString("dd/MM");
            }
            catch (Exception ex)
            {
                return "Không Rõ";
            }
            return str;

        }
        static public int ConvertGoldToMoney(int gold = 0)
        {
            string i = "0";
            try
            {
                switch (gold)
                {
                    case 10:
                        {
                            i = AppSetting.G10;
                            break;
                        }
                    case 30:
                        {
                            i = AppSetting.G30;
                            break;
                        }
                    case 100:
                        {
                            i = AppSetting.G100;
                            break;
                        }
                    case 250:
                        {
                            i = AppSetting.G250;
                            break;
                        }
                    case 600:
                        {
                            i = AppSetting.G600;
                            break;
                        }
                    case 1000:
                        {
                            i = AppSetting.G1000;
                            break;
                        }
                    case 2000:
                        {
                            i = AppSetting.G2000;
                            break;
                        }
                    default:
                        break;
                }

                return Convert.ToInt32(i);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        static public int ConvertMoneyToGold(int pMoney = 0)
        {
            int mGold = 0;
            switch (pMoney)
            {
                case 10000:
                    {
                        mGold = 15;
                        break;
                    }
                case 20000:
                    {
                        mGold = 30;
                        break;
                    }
                case 50000:
                    {
                        mGold = 100;
                        break;
                    }
                case 100000:
                    {
                        mGold = 250;
                        break;
                    }
                case 200000:
                    {
                        mGold = 600;
                        break;
                    }
                case 500000:
                    {
                        mGold = 1800;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return mGold;
        }
    }
}