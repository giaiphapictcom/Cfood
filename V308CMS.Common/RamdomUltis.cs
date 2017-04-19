using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Common
{
    public class RamdomUltis
    {
        static public int layNgauNhienToaDoThamHiemThem(int a)
        {
            int mIntRamdom = 1;
            int total = 0;
            int[] mArray = new int[18] { 1, -1, 2, -2, 3, -3, 4, -4, 5, -5, 6, -6, 7, -7, 8, -8, 9, -9 };
            Random mRamdom = new Random();

            try
            {
                mIntRamdom = mArray[mRamdom.Next(0, 18)];
                total = (mIntRamdom + a);
                if (total > 150)
                {
                    total = 150;
                }
                else if (total < (-150))
                {
                    total = (-150);
                }
                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                throw;

            }

        }
        static public int getRamdom(int pMin,int pMax)
        {
            int mIntRamdom = 1;
            Random mRamdom = new Random();
            try
            {
                mIntRamdom = mRamdom.Next(pMin, pMax);
                return mIntRamdom;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;

            }

        }
        static public int doKhoCuaThamHiem()
        {
            int i = 1;
            Random mRamdom = new Random();
            i = mRamdom.Next(1,4);
            return i;
        }
        static public int KieuQuaTangThamHiem()
        {
            int i = 1;
            DateTime mydate = DateTime.Now;
            if (mydate.Second % 4 == 0)
                i = 3;
            else if (mydate.Second % 4 == 1)
                i = 2;
            else if (mydate.Second % 4 == 1)
                i = 4;
            else if (mydate.Second % 5 == 0)
                i = 5;
            else
                i = 1;
            return i;
        }
        static public int ToaDoThamHiem()
        {
            int i = 1;
            DateTime mydate = DateTime.Now;
            if (mydate.Millisecond < 1000 && mydate.Millisecond >= 950)
                i = 9;
            else if (mydate.Millisecond < 950 && mydate.Millisecond >= 900)
                i = 8;
            else if (mydate.Millisecond < 900 && mydate.Millisecond >= 850)
                i = 7;
            else if (mydate.Millisecond < 850 && mydate.Millisecond >= 800)
                i = 6;
            else if (mydate.Millisecond < 800 && mydate.Millisecond >= 750)
                i = 5;
            else if (mydate.Millisecond < 750 && mydate.Millisecond >= 700)
                i = 4;
            else if (mydate.Millisecond < 700 && mydate.Millisecond >= 650)
                i = 3;
            else if (mydate.Millisecond < 650 && mydate.Millisecond >= 600)
                i = 2;
            else if (mydate.Millisecond < 600 && mydate.Millisecond >= 650)
                i = (-9);
            else if (mydate.Millisecond < 650 && mydate.Millisecond >= 600)
                i = (-8);
            else if (mydate.Millisecond < 600 && mydate.Millisecond >= 550)
                i = (-7);
            else if (mydate.Millisecond < 550 && mydate.Millisecond >= 500)
                i = (-6);
            else if (mydate.Millisecond < 500 && mydate.Millisecond >= 450)
                i = (-5);
            else if (mydate.Millisecond < 450 && mydate.Millisecond >= 400)
                i = (-4);
            else if (mydate.Millisecond < 350 && mydate.Millisecond >= 300)
                i = (-3);
            else
                i = (-2);
            return i;
        }
    }
}