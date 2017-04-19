using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;

namespace V308CMS.Common
{
    public class FilterChat
    {
        public static FilterChat[] All
        {
            get
            {

                /*'truongsa,hoang sa,tham nhũng,tham nhung,thamnhung,trường sa,truong sa,hoàng sa,hoangsa,chiến tranh,chien tranh,chientranh,sex,shit,dick,
                   porn,fuck,tình dục,giao cấu,địt,dit,lồn,buồi,loạn luân,loan luan,soi cầu,lô đề,ho chi minh,cộng sản,congsan,cong san,hồ chí minh,
                   cụ hồ,hochiminh,bác hồ,bacho,bac ho,đảng,đảng cộng sản,dangcongsan,công an,congan,cảnh sát,canhsat,
                   canh sat,lật đổ,latdo,lat do,trung ương,trung uong,nhà nước,bộ truyền thông,bo truyen thong,botruyenthong,nguyentandung,nguyễn tấn dũng,
                   dinhlathang,đinh la thăng,chính trị,chinh tri,cách mạng,cach mang,dm,dmm,dcm,clgt,vkl,vcl,dcmm,chính quyền,lậu,chinh quyen,chinhquyen,chínhquyền,
                   mẹ mày,fuck,địt mẹ mày,địt,lồn,wtf,wth,what the fuck,what the hell,tổ sư\';*/

                ArrayList result = new ArrayList();
                result.Add(new FilterChat("truongsa")); result.Add(new FilterChat("dit")); result.Add(new FilterChat("bác hồ"));
                result.Add(new FilterChat("hoang sa")); result.Add(new FilterChat("lồn")); result.Add(new FilterChat("đảng"));
                result.Add(new FilterChat("tham nhũng")); result.Add(new FilterChat("buồi")); result.Add(new FilterChat("đảng cộng sản"));
                result.Add(new FilterChat("tham nhung")); result.Add(new FilterChat("loạn luân")); result.Add(new FilterChat("dangcongsan"));
                result.Add(new FilterChat("trường sa")); result.Add(new FilterChat("loan luan")); result.Add(new FilterChat("nhà nước"));
                result.Add(new FilterChat("truong sa")); result.Add(new FilterChat("ho chi minh")); result.Add(new FilterChat("bộ truyền thông"));
                result.Add(new FilterChat("hoàng sa")); result.Add(new FilterChat("cộng sản")); result.Add(new FilterChat("nguyentandung"));
                result.Add(new FilterChat("nguyen tan dung")); result.Add(new FilterChat("sóc lọ")); result.Add(new FilterChat("quay tay"));
                result.Add(new FilterChat("shit")); result.Add(new FilterChat("congsan")); result.Add(new FilterChat("nguyễn tấn dũng"));
                result.Add(new FilterChat("dick")); result.Add(new FilterChat("cong san")); result.Add(new FilterChat("đinh la thăng"));
                result.Add(new FilterChat("porn")); result.Add(new FilterChat("hồ chí minh")); result.Add(new FilterChat("dm"));
                result.Add(new FilterChat("fuck")); result.Add(new FilterChat("cụ hồ")); result.Add(new FilterChat("dmm"));
                result.Add(new FilterChat("địt")); result.Add(new FilterChat("hochiminh")); result.Add(new FilterChat("cụ mày"));
                result.Add(new FilterChat("clgt")); result.Add(new FilterChat("wth")); result.Add(new FilterChat("dcm"));
                result.Add(new FilterChat("vkl")); result.Add(new FilterChat("what the fuck")); result.Add(new FilterChat("bố mày"));
                result.Add(new FilterChat("dcmm")); result.Add(new FilterChat("what the hell")); result.Add(new FilterChat("quay tay"));
                result.Add(new FilterChat("chính quyền")); result.Add(new FilterChat("tổ sư")); result.Add(new FilterChat("sóc lọ"));
                result.Add(new FilterChat("lậu")); result.Add(new FilterChat("tổ bà")); result.Add(new FilterChat("buồi"));
                result.Add(new FilterChat("mẹ mày")); result.Add(new FilterChat("cứt")); result.Add(new FilterChat("truong tan sang"));
                result.Add(new FilterChat("địt mẹ mày")); result.Add(new FilterChat("kứt")); result.Add(new FilterChat("trương tấn sang"));
                result.Add(new FilterChat("bà mày")); 
                result.Add(new FilterChat("wtf")); 
                if (result.Count == 0)
                    return null;
                else
                    return (FilterChat[])result.ToArray(typeof(FilterChat));
            }
        }

        public static string Format(string input)
        {
            if (input == null || input.Length == 0)
            {
                return input;
            }
            else
            {
                StringBuilder result = new StringBuilder(input);

                FilterChat[] all = All;
                foreach (FilterChat myFilterChat in all)
                {
                    result = result.Replace(myFilterChat.Shortcut,"[****]");
                }

                return result.ToString();
            }
        }



        #region Constructors.
        // ------------------------------------------------------------------

        public FilterChat(FilterChat src)
        {
            Shortcut = src.Shortcut;
          
        }

        public FilterChat( string shortcut)
        {
            Shortcut = shortcut;

        }

      
        // ------------------------------------------------------------------
        #endregion

        #region Properties.
        // ------------------------------------------------------------------

        /// <summary>
        /// The (case-sensitive!) string to be replaced with the emoticon.
        /// </summary>
        public string Shortcut = "";

        /// <summary>
        /// The filename (no path) of the emoticon.
        /// </summary>
       
        /// <summary>
        /// The optional URL of the emoticon. If specified, the emoticon
        /// can be clicked.
        /// </summary>
    

        // ------------------------------------------------------------------
        #endregion

        #region Internal helper.
        // ------------------------------------------------------------------

        /// <summary>
        /// Returns the complete virtual path.
        /// </summary>
        

        /// <summary>
        /// Get the root of the current web application.
        /// Expands a "~" character by the real path.
        /// </summary>
        

        // ------------------------------------------------------------------
        #endregion

    }
}