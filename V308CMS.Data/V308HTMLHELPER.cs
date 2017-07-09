using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using V308CMS.Common;

namespace V308CMS.Data
{
    public class V308HTMLHELPER
    {
        public static string TaoDanhSachTinTuc(List<News> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tiêu đề</th><th scope=\"col\" class=\"rgHeader\">Số từ</th><th scope=\"col\" class=\"rgHeader\">Views</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Hot</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (News it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Title + "</td>");
                        mStr.Append("<td>" + it.Detail.Count() + " từ</td>");
                        mStr.Append("<td>" + it.Views + " lượt</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><input type=\"checkbox\" name=\"pcheckbox\"></td>");
                        mStr.Append("<td><a class=\"green\" href=\"/News/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        if (it.Status == true)
                            mStr.Append("<a href=\"javascript:Admin_News_HienThiPopUpAnTinTuc('Bạn có thực sự muốn ẩn tin này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon.png\" /></a>");
                        else
                            mStr.Append("<a href=\"javascript:Admin_News_HienThiPopUpHienTinTuc('Bạn có thực sự muốn hiện tin này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon2.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_News_HienThiPopUp('Bạn có thực sự muốn xóa tin này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Title + "</td>");
                        mStr.Append("<td>" + it.Detail.Count() + " từ</td>");
                        mStr.Append("<td>" + it.Views + " lượt</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><input type=\"checkbox\" name=\"pcheckbox\"></td>");
                        mStr.Append("<td><a class=\"green\" href=\"/News/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        if (it.Status == true)
                            mStr.Append("<a href=\"javascript:Admin_News_HienThiPopUpAnTinTuc('Bạn có thực sự muốn ẩn tin này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon.png\" /></a>");
                        else
                            mStr.Append("<a href=\"javascript:Admin_News_HienThiPopUpHienTinTuc('Bạn có thực sự muốn hiện tin này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon2.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_News_HienThiPopUp('Bạn có thực sự muốn xóa tin này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachCacNhomTinTuc(List<NewsGroups> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Kích hoạt</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (NewsGroups it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><input type=\"checkbox\" name=\"pcheckbox\"></td>");
                        mStr.Append("<td><a class=\"green\" href=\"/NewsCategory/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        if (it.Status == true)
                            mStr.Append("<a href=\"javascript:Admin_NewsGroup_HienThiPopUpAnTinTuc('Bạn có thực sự muốn ẩn loại tin này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon.png\" /></a>");
                        else
                            mStr.Append("<a href=\"javascript:Admin_NewsGroup_HienThiPopUpHienTinTuc('Bạn có thực sự muốn hiện loại tin này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon2.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_NewsGroup_HienThiPopUp('Bạn có thực sự muốn xóa loại tin này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><input type=\"checkbox\" name=\"pcheckbox\"></td>");
                        mStr.Append("<td><a class=\"green\" href=\"/NewsCategory/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        if (it.Status == true)
                            mStr.Append("<a href=\"javascript:Admin_NewsGroup_HienThiPopUpAnTinTuc('Bạn có thực sự muốn ẩn loại tin này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon.png\" /></a>");
                        else
                            mStr.Append("<a href=\"javascript:Admin_NewsGroup_HienThiPopUpHienTinTuc('Bạn có thực sự muốn hiện loại tin này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon2.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_NewsGroup_HienThiPopUp('Bạn có thực sự muốn xóa loại tin này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomTin(List<NewsGroups> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<NewsGroups> pList1;
            List<NewsGroups> pList2;
            List<NewsGroups> pList3;
            List<NewsGroups> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (NewsGroups it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (NewsGroups ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (NewsGroups mt in pList3)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (NewsGroups ht in pList4)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomTinHome(List<NewsGroups> pList, int pPage, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<NewsGroups> pList1;
            List<NewsGroups> pList2;
            List<NewsGroups> pList3;
            List<NewsGroups> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupIdHome\"   onchange=\"javascript:ThayDoiKieuTinTuc(" + pPage + ");\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (NewsGroups it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (NewsGroups ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (NewsGroups mt in pList3)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (NewsGroups ht in pList4)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachMenu(List<NewsGroups> pList)
        {
            StringBuilder mStr = new StringBuilder();
            List<NewsGroups> pList1;
            List<NewsGroups> pList2;
            List<NewsGroups> pList3;
            List<NewsGroups> pList4;
            try
            {
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 1
                          orderby p.Number ascending
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (NewsGroups it in pList1)
                    {
                        if (it.Link.Length > 1)
                            mStr.Append("<li class=\"li\"><a href=\"" + it.Link + "\">" + it.Name + "</a>");
                        else
                            mStr.Append("<li class=\"li\"><a href=\"/" + Ultility.LocDau(it.Name) + "-group" + it.ID + ".html\">" + it.Name + "</a>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  orderby p.Number ascending
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {
                            mStr.Append("<ul class=\"ul1\">");
                            foreach (NewsGroups ut in pList2)
                            {
                                if (ut.Link.Length > 1)
                                    mStr.Append("<li class=\"li\"><a href=\"" + ut.Link + "\">" + ut.Name + "</a>");
                                else
                                    mStr.Append("<li class=\"li\"><a href=\"/" + Ultility.LocDau(ut.Name) + "-group" + ut.ID + ".html\">" + ut.Name + "</a>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList2
                                          where p.Parent == ut.ID
                                          orderby p.Number ascending
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {
                                    mStr.Append("<ul class=\"ul2\">");
                                    foreach (NewsGroups mt in pList3)
                                    {
                                        if (mt.Link.Length > 1)
                                            mStr.Append("<li class=\"li\"><a href=\"" + mt.Link + "\">" + mt.Name + "</a>");
                                        else
                                            mStr.Append("<li class=\"li\"><a href=\"/" + Ultility.LocDau(mt.Name) + "-group" + mt.ID + ".html\">" + mt.Name + "</a>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList3
                                                  where p.Parent == mt.ID
                                                  orderby p.Number ascending
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {
                                            mStr.Append("<ul class=\"ul3\">");
                                            foreach (NewsGroups ht in pList4)
                                            {
                                                if (ht.Link.Length > 1)
                                                    mStr.Append("<li class=\"li\"><a href=\"" + ht.Link + "\">" + ht.Name + "</a>");
                                                else
                                                    mStr.Append("<li class=\"li\"><a href=\"/" + Ultility.LocDau(ht.Name) + "-group" + ht.ID + ".html\">" + ht.Name + "</a>");
                                            }
                                            mStr.Append("</ul>");
                                        }
                                        mStr.Append("</li>");
                                    }
                                    mStr.Append("</ul>");
                                }
                                mStr.Append("</li>");
                            }
                            mStr.Append("</ul>");
                        }
                        mStr.Append("</li>");
                    }
                }
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomTinHome2(List<NewsGroups> pList, int pPage, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<NewsGroups> pList1;
            List<NewsGroups> pList2;
            List<NewsGroups> pList3;
            List<NewsGroups> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupIdHome\"   onchange=\"javascript:ThayDoiKieuTinTuc2(" + pPage + ");\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (NewsGroups it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (NewsGroups ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (NewsGroups mt in pList3)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (NewsGroups ht in pList4)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomTin2(List<NewsGroups> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<NewsGroups> pList1;
            List<NewsGroups> pList2;
            List<NewsGroups> pList3;
            List<NewsGroups> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (NewsGroups it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (NewsGroups ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (NewsGroups mt in pList3)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (NewsGroups ht in pList4)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomTin3(List<NewsGroups> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<NewsGroups> pList1;
            List<NewsGroups> pList2;
            List<NewsGroups> pList3;
            List<NewsGroups> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (NewsGroups it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (NewsGroups ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (NewsGroups mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (NewsGroups ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachCacNhomAnh(List<ImageType> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Kích cỡ</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (ImageType it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.Size + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/ImageType/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_ImagesGroup_HienThiPopUp('Bạn có thực sự muốn xóa loại ảnh này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.Size + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/ImageType/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_ImagesGroup_HienThiPopUp('Bạn có thực sự muốn xóa loại ảnh này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomAnh2(List<ImageType> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<ImageType> pList1;
            List<ImageType> pList2;
            List<ImageType> pList3;
            List<ImageType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (ImageType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (ImageType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (ImageType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (ImageType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomAnh3(List<ImageType> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<ImageType> pList1;
            List<ImageType> pList2;
            List<ImageType> pList3;
            List<ImageType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (ImageType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (ImageType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (ImageType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (ImageType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomAnh4(List<ImageType> pList, int pId)
        {

            StringBuilder mStr = new StringBuilder();
            List<ImageType> pList1;
            List<ImageType> pList2;
            List<ImageType> pList3;
            List<ImageType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại ảnh</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (ImageType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (ImageType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (ImageType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (ImageType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachCacAnh(List<Image> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Ảnh</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (Image it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><img class=\"adminimage\" src=\"" + it.LinkImage + "\"/></td>");
                        mStr.Append("<td><a class=\"green\" href=\"/Image/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_Images_HienThiPopUp('Bạn có thực sự muốn xóa ảnh này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><img class=\"adminimage\" src=\"" + it.LinkImage + "\"/></td>");
                        mStr.Append("<td><a class=\"green\" href=\"/Image/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_Images_HienThiPopUp('Bạn có thực sự muốn xóa  ảnh này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }


        public static string TaoDanhSachCacNhomFile(List<FileType> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (FileType it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/ImageType/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_FileGroup_HienThiPopUp('Bạn có thực sự muốn xóa loại File này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/ImageType/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_FileGroup_HienThiPopUp('Bạn có thực sự muốn xóa loại File này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomFile2(List<FileType> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<FileType> pList1;
            List<FileType> pList2;
            List<FileType> pList3;
            List<FileType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.ParentID == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (FileType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.ParentID == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (FileType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.ParentID == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (FileType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.ParentID == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (FileType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomFile3(List<FileType> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<FileType> pList1;
            List<FileType> pList2;
            List<FileType> pList3;
            List<FileType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.ParentID == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (FileType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.ParentID == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (FileType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.ParentID == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (FileType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.ParentID == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (FileType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomFile4(List<FileType> pList, int pId)
        {

            StringBuilder mStr = new StringBuilder();
            List<FileType> pList1;
            List<FileType> pList2;
            List<FileType> pList3;
            List<FileType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại ảnh</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.ParentID == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (FileType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.ParentID == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (FileType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.ParentID == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (FileType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.ParentID == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (FileType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachCacFile(List<File> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Giá trị</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (File it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FileName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.Value + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/File/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_File_HienThiPopUp('Bạn có thực sự muốn xóa File này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FileName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.Value + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/File/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_File_HienThiPopUp('Bạn có thực sự muốn File  ảnh này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomFileHome(List<FileType> pList, int pPage, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<FileType> pList1;
            List<FileType> pList2;
            List<FileType> pList3;
            List<FileType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupIdHome\"   onchange=\"javascript:ThayDoiKieuNhomFile(" + pPage + ");\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.ParentID == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (FileType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.ParentID == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (FileType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.ParentID == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (FileType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.ParentID == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (FileType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomFileHome2(List<FileType> pList, int pPage, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<FileType> pList1;
            List<FileType> pList2;
            List<FileType> pList3;
            List<FileType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupIdHome\"   onchange=\"javascript:ThayDoiKieuNhomFile2(" + pPage + ");\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.ParentID == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (FileType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.ParentID == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (FileType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.ParentID == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (FileType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.ParentID == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (FileType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }


        public static string TaoDanhSachProductDistributor(List<ProductDistributor> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Ảnh</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (ProductDistributor it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><img class=\"adminimage\" src=\"" + it.Image + "\"/></td>");
                        mStr.Append("<td><a class=\"green\" href=\"/ProductDistributor/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_ProductDistributor_HienThiPopUp('Bạn có thực sự muốn xóa nhà phân phối này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><img class=\"adminimage\" src=\"" + it.Image + "\"/></td>");
                        mStr.Append("<td><a class=\"green\" href=\"/ProductDistributor/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_ProductDistributor_HienThiPopUp('Bạn có thực sự muốn xóa nhà phân phối này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachProductManufacturer(List<ProductManufacturer> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Ảnh</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (ProductManufacturer it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><img class=\"adminimage\" src=\"" + it.Image + "\"/></td>");
                        mStr.Append("<td><a class=\"green\" href=\"/ProductManufacturer/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_ProductManufacturer_HienThiPopUp('Bạn có thực sự muốn xóa nhà sản xuất này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><img class=\"adminimage\" src=\"" + it.Image + "\"/></td>");
                        mStr.Append("<td><a class=\"green\" href=\"/ProductManufacturer/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_ProductManufacturer_HienThiPopUp('Bạn có thực sự muốn xóa nhà sản xuất này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachProductType(List<ProductType> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ghi chú</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (ProductType it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + it.Description + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");

                        mStr.Append("<td><a class=\"green\" href=\"/ProductType/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_ProductType_HienThiPopUp('Bạn có thực sự muốn xóa loại sản phẩm này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + it.Description + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");

                        mStr.Append("<td><a class=\"green\" href=\"/ProductType/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_ProductType_HienThiPopUp('Bạn có thực sự muốn xóa loại sản phẩm này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachProductType2(List<ProductType> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<ProductType> pList1;
            List<ProductType> pList2;
            List<ProductType> pList3;
            List<ProductType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (ProductType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (ProductType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (ProductType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (ProductType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachProductType3(List<ProductType> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<ProductType> pList1;
            List<ProductType> pList2;
            List<ProductType> pList3;
            List<ProductType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (ProductType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (ProductType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (ProductType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (ProductType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }

        public static string TaoDanhSachSanPham(List<Product> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col><col><col><col></colgroup>");
                mStr.Append("<thead>");
                mStr.Append("<tr>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">Mã</th>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">Tiêu đề</th>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">Ngày đăng</th>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">Ảnh</th>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">Giá</th>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">Mã SP</th>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">Khuyến mãi</th>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">Vị trí</th>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">Hiện TC</th>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">SPBC</th>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">Hiện</th>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">Xóa</th>");
                mStr.Append("<th scope=\"col\" class=\"rgHeader\">Action</th>");
                mStr.Append("</tr>");
                mStr.Append("</thead>");
                mStr.Append("<tbody>");
                foreach (Product it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td><div><input type=\"hidden\" name=\"pId\" value=\"" + it.ID + "\"/>" + it.ID + "</div></td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><img class=\"adminimage\" src=\"" + it.Image + "\"/></td>");
                        mStr.Append("<td>" + String.Format("{0: 0,0}", (it.Price)) + "</td>");
                        mStr.Append("<td>CF" + it.ID + "</td>");
                        mStr.Append("<td>" + it.SaleOff + " %</td>");
                        mStr.Append("<td><input  class=\"cbsp\" type=\"text\" name=\"pNumber\" value=\"" + it.Number + "\"/></td>");
                        mStr.Append("<td><input type=\"checkbox\" " + ConverterUlti.ConvertLogicToString(it.Hot) + "   name=\"pHome\"  value=\"\"/></td>");
                        mStr.Append("<td><input type=\"checkbox\" " + ConverterUlti.ConvertLogicToString(it.Visible) + " name=\"pBestSale\"  value=\"\"/></td>");
                        mStr.Append("<td><input type=\"checkbox\"" + ConverterUlti.ConvertLogicToString(it.Status) + "  name=\"pHide\"  value=\"true\"/></td>");
                        mStr.Append("<td><input type=\"checkbox\"name=\"pDelete\"  value=\"\"/></td>");
                        mStr.Append("<td><a class=\"green\" href=\"/Product/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        if (it.Status == true)
                            mStr.Append("<a href=\"javascript:Admin_Product_HienThiPopUpAn('Bạn có thực sự muốn ẩn sản phẩm này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon.png\" /></a>");
                        else
                            mStr.Append("<a href=\"javascript:Admin_Product_HienThiPopUpHien('Bạn có thực sự muốn hiện sản phẩm này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon2.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_Product_HienThiPopUp('Bạn có thực sự muốn xóa sản phẩm này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td><div><input type=\"hidden\" name=\"pId\" value=\"" + it.ID + "\"/>" + it.ID + "</div></td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><img class=\"adminimage\" src=\"" + it.Image + "\"/></td>");
                        mStr.Append("<td>" + String.Format("{0: 0,0}", (it.Price)) + "</td>");
                        mStr.Append("<td>CF" + it.ID + "</td>");
                        mStr.Append("<td>" + it.SaleOff + " %</td>");
                        mStr.Append("<td><input  class=\"cbsp\" type=\"text\" name=\"pNumber\" value=\"" + it.Number + "\"/></td>");
                        mStr.Append("<td><input type=\"checkbox\" " + ConverterUlti.ConvertLogicToString(it.Hot) + "   name=\"pHome\"  value=\"\"/></td>");
                        mStr.Append("<td><input type=\"checkbox\" " + ConverterUlti.ConvertLogicToString(it.Visible) + " name=\"pBestSale\"  value=\"\"/></td>");
                        mStr.Append("<td><input type=\"checkbox\"" + ConverterUlti.ConvertLogicToString(it.Status) + "  name=\"pHide\"  value=\"true\"/></td>");
                        mStr.Append("<td><input type=\"checkbox\" name=\"pDelete\"  value=\"\"/></td>");
                        mStr.Append("<td><a class=\"green\" href=\"/Product/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        if (it.Status == true)
                            mStr.Append("<a href=\"javascript:Admin_Product_HienThiPopUpAn('Bạn có thực sự muốn ẩn sản phẩm này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon.png\" /></a>");
                        else
                            mStr.Append("<a href=\"javascript:Admin_Product_HienThiPopUpHien('Bạn có thực sự muốn hiện sản phẩm này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon2.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_Product_HienThiPopUp('Bạn có thực sự muốn xóa sản phẩm này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachProductType4(List<ProductType> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<ProductType> pList1;
            List<ProductType> pList2;
            List<ProductType> pList3;
            List<ProductType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (ProductType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (ProductType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (ProductType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (ProductType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachProductDistributor4(List<ProductDistributor> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pProductDistributor\" id=\"pProductDistributorId\">");
                foreach (ProductDistributor it in pList)
                {
                    if (it.ID == pId)
                        mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                    else
                        mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachProductManufacturer4(List<ProductManufacturer> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pProductManufacturer\" id=\"pProductManufacturerId\">");
                foreach (ProductManufacturer it in pList)
                {
                    if (it.ID == pId)
                        mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                    else
                        mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachMarket(List<Market> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pMarket\" id=\"pMarketId\">");
                foreach (Market it in pList)
                {
                    if (it.ID == pId)
                        mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.UserName + "</option>");
                    else
                        mStr.Append("<option value=\"" + it.ID + "\">" + it.UserName + "</option>");
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }

        public static string TaoDanhSachProductTypeHome(List<ProductType> pList, int pPage, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<ProductType> pList1;
            List<ProductType> pList2;
            List<ProductType> pList3;
            List<ProductType> pList4;
            try
            {
                mStr.Append("<select style=\"width:250px;\" name=\"pType\" id=\"pGroupIdHome\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (ProductType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (ProductType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (ProductType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (ProductType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachProductTypeHome2(List<ProductType> pList, int pPage, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<ProductType> pList1;
            List<ProductType> pList2;
            List<ProductType> pList3;
            List<ProductType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupIdHome\"   onchange=\"javascript:ThayDoiKieuSanPham2(" + pPage + ");\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (ProductType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (ProductType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (ProductType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (ProductType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachSupportType(List<SupportType> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (SupportType it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/SupportType/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_SupportType_HienThiPopUp('Bạn có thực sự muốn xóa kiểu hỗ trợ này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/SupportType/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_SupportType_HienThiPopUp('Bạn có thực sự muốn xóa kiểu hỗ trợ này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachSupport(List<Support> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Nick</th><th scope=\"col\" class=\"rgHeader\">Mobile</th><th scope=\"col\" class=\"rgHeader\">Email</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (Support it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.Nick + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + it.Email + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/Support/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_Support_HienThiPopUp('Bạn có thực sự muốn xóa hỗ trợ này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.Name + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.Nick + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + it.Email + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/Support/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_Support_HienThiPopUp('Bạn có thực sự muốn xóa hỗ trợ này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachSupportType2(List<SupportType> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 0;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                foreach (SupportType it in pList)
                {
                    if (i == 0)
                    {
                        mStr.Append("<option value=\"0\"  selected=\"selected\">Chọn nhóm tin mẹ</option>");
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                    }
                    else
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                    }
                    i++;
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachSupportType3(List<SupportType> pList, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            bool mck = false;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupId\">");
                if (pId == 0 && mck == false)
                {
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Chọn nhóm tin mẹ</option>");
                    mck = true;
                }
                else if (pId != 0 && mck == false)
                {
                    mStr.Append("<option value=\"0\">Chọn nhóm tin mẹ</option>");
                    mck = true;
                }
                foreach (SupportType it in pList)
                {
                    if (it.ID == pId)
                        mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                    else
                        mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                    i++;
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomAnhHome(List<ImageType> pList, int pPage, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<ImageType> pList1;
            List<ImageType> pList2;
            List<ImageType> pList3;
            List<ImageType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupIdHome\"   onchange=\"javascript:ThayDoiKieuNhomAnh(" + pPage + ");\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (ImageType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (ImageType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (ImageType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (ImageType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachNhomAnhHome2(List<ImageType> pList, int pPage, int pId)
        {
            StringBuilder mStr = new StringBuilder();
            List<ImageType> pList1;
            List<ImageType> pList2;
            List<ImageType> pList3;
            List<ImageType> pList4;
            try
            {
                mStr.Append("<select style=\"width:412px;\" name=\"pGroup\" id=\"pGroupIdHome\"   onchange=\"javascript:ThayDoiKieuNhomAnh2(" + pPage + ");\">");
                if (pId == 0)
                    mStr.Append("<option value=\"0\"  selected=\"selected\">Tất cả các loại</option>");
                else
                    mStr.Append("<option value=\"0\">Tất cả các loại</option>");
                //lay danh sach cac nhom cap 1
                pList1 = (from p in pList
                          where p.Parent == 0
                          select p).ToList();
                if (pList1.Count > 0)
                {
                    foreach (ImageType it in pList1)
                    {
                        if (it.ID == pId)
                            mStr.Append("<option value=\"" + it.ID + "\"  selected=\"selected\">" + it.Name + "</option>");
                        else
                            mStr.Append("<option value=\"" + it.ID + "\">" + it.Name + "</option>");
                        //lay danh sach cac group ben trong
                        pList2 = (from p in pList
                                  where p.Parent == it.ID
                                  select p).ToList();
                        if (pList2.Count > 0)
                        {

                            foreach (ImageType ut in pList2)
                            {
                                if (ut.ID == pId)
                                    mStr.Append("<option value=\"" + ut.ID + "\"  selected=\"selected\">----" + ut.Name + "</option>");
                                else
                                    mStr.Append("<option value=\"" + ut.ID + "\">----" + ut.Name + "</option>");
                                //lay danh sach cac group ben trong
                                pList3 = (from p in pList
                                          where p.Parent == ut.ID
                                          select p).ToList();
                                if (pList3.Count > 0)
                                {

                                    foreach (ImageType mt in pList2)
                                    {
                                        if (mt.ID == pId)
                                            mStr.Append("<option value=\"" + mt.ID + "\"  selected=\"selected\">------" + mt.Name + "</option>");
                                        else
                                            mStr.Append("<option value=\"" + mt.ID + "\">------" + mt.Name + "</option>");
                                        //lay danh sach cac group ben trong
                                        pList4 = (from p in pList
                                                  where p.Parent == mt.ID
                                                  select p).ToList();
                                        if (pList4.Count > 0)
                                        {

                                            foreach (ImageType ht in pList2)
                                            {
                                                if (ht.ID == pId)
                                                    mStr.Append("<option value=\"" + ht.ID + "\"  selected=\"selected\">--------" + ht.Name + "</option>");
                                                else
                                                    mStr.Append("<option value=\"" + ht.ID + "\">--------" + ht.Name + "</option>");
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
                mStr.Append("</select>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }

        public static string TaoDanhSachSlideAnh(List<ProductImage> pList, Product pImageFirst, string SliderName)
        {
            StringBuilder mStr = new StringBuilder();
            try
            {

                if (pList.Count > 0 || !String.IsNullOrEmpty(pImageFirst.Image))
                {
                    mStr.Append("<img style=\"max-width:740px;\" title=\"" + pImageFirst.Name + "\" src=\"" + pImageFirst.Image + "\"  alt=\"" + pImageFirst.Name + "\" title=\"" + pImageFirst.Name + "\" /><br/>");
                    foreach (ProductImage it in pList)
                    {
                        mStr.Append("<img style=\"max-width:740px;\" title=\"" + it.Name + "\" src=\"" + it.Name + "\"  alt=\"" + it.Name + "\" title=\"" + it.Name + "\" /><br/>");
                    }
                }
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachKetQuaTimKiem(List<Product> pList, string pKey, int pType)
        {
            StringBuilder mStr = new StringBuilder();
            try
            {

                if (pList.Count > 0)
                {
                    mStr.Append("<div class=\"grounpStoryHome\">");
                    mStr.Append("<div class=\"titleCatalog\"><h2 class=\"tActive\"><a href=\"/Home/Search?txtSearch=" + pKey + "&pType=" + pType + "\">Kết quả tìm kiếm : " + pKey + "</a></h2><span class=\"sub_catalog\">");
                    mStr.Append("</span></div>");
                    //Ket thuc tao header
                    //tao danh sach san pham
                    mStr.Append("<div class=\"TabListingHome\">");
                    //Tao dnh sach san pham cho nhom me
                    mStr.Append("<div class=\"middle\">");
                    mStr.Append("<div><div class=\"listing\">");
                    foreach (Product it in pList)
                    {
                        mStr.Append("<div class=\"story\">");
                        mStr.Append("<div class=\"image_container\">");
                        mStr.Append("<div class=\"image_cell\">");
                        mStr.Append("<a title=\"\" href=\"/Home/ChiTietSanPham?pId=" + it.ID + "\" rel=\"tooltip\">");
                        mStr.Append("<img alt=\"Điện thoại di động Sony Xperia Z1 C6902\" src=\"" + it.Image + "\" class=\"image_news\">");
                        mStr.Append("</a>");
                        mStr.Append("</div>");
                        mStr.Append("</div>");
                        mStr.Append("<h4 class=\"title\">");
                        mStr.Append("<a title=\"Điện thoại di động Sony Xperia Z1 C6902\" href=\"/Home/ChiTietSanPham?pId=" + it.ID + "\">" + it.Name + "</a>");
                        mStr.Append("</h4>");
                        mStr.Append("<p class=\"price\">" + it.Price + " VNĐ</p>");
                        mStr.Append("</div>");
                    }
                    mStr.Append("</div></div>");//ket thuc phia trai
                    mStr.Append("</div>");
                    mStr.Append("</div>");
                }
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachKetQuaTimKiemNhaSanXuat(List<ProductManufacturer> pList, int pGroupId)
        {
            StringBuilder mStr = new StringBuilder();
            try
            {

                if (pList.Count > 0)
                {
                    foreach (ProductManufacturer it in pList)
                    {
                        mStr.Append("<li><a href=\"/Home/Search?pType=2&pValue1=" + it.ID + "&pGroupId=" + pGroupId + "\">" + it.Name + "</a></li>");
                    }
                }
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }


        public static string TaoDanhSachSanPhamBanChay(List<Product> mList, string pDisplay)
        {
            StringBuilder mStr = new StringBuilder();
            try
            {
                //Tao dnh sach san pham cho nhom me
                mStr.Append("<div class=\"listingTabHome\" style=\"display: " + pDisplay + ";\">");
                mStr.Append("<div class=\"leftTabHome\"><div class=\"listing\">");
                for (int i = 0; i < 8; i++)
                {
                    if (i < mList.Count)
                    {
                        mStr.Append("<div class=\"story\">");
                        mStr.Append("<div class=\"image_container\">");
                        mStr.Append("<div class=\"image_cell\">");
                        mStr.Append("<a title=\"\" href=\"/chi-tiet-san-pham/" + mList[i].ID + "/" + Ultility.LocDau(mList[i].Name) + "\" rel=\"tooltip\">");
                        mStr.Append("<img alt=\"Điện thoại di động Sony Xperia Z1 C6902\" src=\"" + mList[i].Image + "\" class=\"image_news\">");
                        mStr.Append("</a>");
                        mStr.Append("</div>");
                        mStr.Append("</div>");
                        mStr.Append("<h4 class=\"title\">");
                        mStr.Append("<a title=\"Điện thoại di động Sony Xperia Z1 C6902\" href=\"/chi-tiet-san-pham/" + mList[i].ID + "/" + Ultility.LocDau(mList[i].Name) + "\">" + mList[i].Name + "</a>");
                        mStr.Append("</h4>");
                        if (mList[i].SaleOff > 0)
                        {
                            mStr.Append("<p class=\"price_basic\">" + (mList[i].Price) + " VNĐ</p>");
                            mStr.Append("<p class=\"price\">" + (mList[i].Price - ((mList[i].Price / 100) * mList[i].SaleOff)) + " VNĐ</p>");
                            mStr.Append("<div class=\"image_saleoff\"><img src=\"/Content/Images/km.png\"><span>-" + mList[i].SaleOff + "%</span></div>");
                        }
                        else
                        {
                            mStr.Append("<p class=\"price\">" + mList[i].Price + " VNĐ</p>");
                        }
                        mStr.Append("</div>");

                    }

                }
                mStr.Append("</div></div>");//ket thuc phia trai
                if (mList.Count < 10 && mList.Count >= 9)
                {
                    //bat dau phia phai
                    mStr.Append("<div class=\"rightTabHome\">");
                    mStr.Append("<div class=\"story laststory\">");
                    mStr.Append("<div class=\"image_laststory\">");
                    mStr.Append("<div class=\"lastimage\">");
                    mStr.Append("<a title=\"\" href=\"/chi-tiet-san-pham/" + mList[8].ID + "/" + Ultility.LocDau(mList[8].Name) + "\" rel=\"tooltip\">");
                    mStr.Append("<img alt=\"Điện thoại di động Sony Xperia Z1 C6902\" src=\"" + mList[8].Image + "\" class=\"lastimage_news\">");
                    mStr.Append("</div>");
                    mStr.Append("</div>");
                    mStr.Append("<h4 class=\"title\">");
                    mStr.Append("<a title=\"Điện thoại di động Sony Xperia Z1 C6902\" href=\"/chi-tiet-san-pham/" + mList[8].ID + "/" + Ultility.LocDau(mList[8].Name) + "\">" + mList[8].Name + "</a>");
                    mStr.Append("</h4>");
                    if (mList[8].SaleOff > 0)
                    {
                        mStr.Append("<p class=\"price_basic\">" + (mList[8].Price) + " VNĐ</p>");
                        mStr.Append("<p class=\"price\">" + (mList[8].Price - ((mList[8].Price / 100) * mList[8].SaleOff)) + " VNĐ</p>");
                        mStr.Append("<div class=\"image_saleoff\"><img src=\"/Content/Images/km.png\"><span>-" + mList[8].SaleOff + "%</span></div>");
                    }
                    else
                    {
                        mStr.Append("<p class=\"price\">" + mList[8].Price + " VNĐ</p>");
                    }
                    mStr.Append("</div>");
                }
                mStr.Append("</div>");
                //ket thuc phia phai
                mStr.Append("</div>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachSanPhamGioHang(List<Product> mList)
        {
            StringBuilder mStr = new StringBuilder();
            try
            {
                //Tao dnh sach san pham cho nhom m
                foreach (Product it in mList)
                {
                    mStr.Append("<tr class=\"tr_s_no\">");
                    mStr.Append("<td class=\"td_image\">");
                    mStr.Append("<img width=\"50\" height=\"50\" src=\"" + it.Image + "\" />");
                    mStr.Append("</td>");
                    mStr.Append("<td class=\"td_right\"><a href=\"#\" title=\"" + it.Name + "\">" + it.Name + "</a>");
                    mStr.Append("<input type=\"hidden\" id=\"hide_price_item_929\" value=\"" + it.Price + "\" />");
                    mStr.Append("</td>");
                    mStr.Append("<td align=\"right\" class=\"td_price\">" + it.Price + "  VNĐ</td>");
                    mStr.Append("<td align=\"right\" class=\"td_quantity\">");
                    mStr.Append("1");
                    mStr.Append("</td>");
                    mStr.Append("<td align=\"right\" class=\"td_last\">");
                    mStr.Append("<div id=\"total_price_929\">" + it.Price + "  VNĐ </div>");
                    mStr.Append("</td>");
                    mStr.Append("</tr>");
                }
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachSanPhamGioHangAdmin(List<Product> mList)
        {
            StringBuilder mStr = new StringBuilder();
            try
            {
                mStr.Append("<table cellspacing=\"0\" border=\"0\" width=\"100%\"><col width=\"10%\" />");
                mStr.Append("<col width=\"30%\" />");
                mStr.Append("<col width=\"15%\" />");
                mStr.Append("<col width=\"15%\" />");
                mStr.Append("<col width=\"20%\" />");
                mStr.Append("<tr class=\"tr_shop\">");
                mStr.Append("<th class=\"td_nobor_center\" scope=\"col\" colspan=\"2\">Sản phẩm</th>");
                mStr.Append("<th class=\"td_price\" scope=\"col\">Giá bán</th>");
                mStr.Append("<th class=\"td_quantity\" scope=\"col\">Số lượng</th>");
                mStr.Append("<th class=\"td_quantity\" scope=\"col\">Khuyến mãi</th>");
                mStr.Append("<th class=\"td_last\" scope=\"col\">Thành tiền(VND)</th>");
                mStr.Append("</tr>");
                //Tao dnh sach san pham cho nhom m
                foreach (Product it in mList)
                {
                    mStr.Append("<tr class=\"tr_s_no\">");
                    mStr.Append("<td class=\"td_image\">");
                    mStr.Append("<img width=\"50\" height=\"50\" src=\"" + it.Image + "\" />");
                    mStr.Append("</td>");
                    mStr.Append("<td class=\"td_right\"><a  target=\"_blank\"  href=\"/" + Ultility.LocDau(it.Name) + "-d" + it.ID + ".html\" title=\"" + it.Name + "\">" + it.Name + "</a>");
                    mStr.Append("<input type=\"hidden\" id=\"hide_price_item_929\" value=\"" + it.Price + "\" />");
                    mStr.Append("</td>");
                    mStr.Append("<td align=\"right\" class=\"td_price\">" + String.Format("{0: 0,0}", (it.Price)) + "  VNĐ</td>");
                    mStr.Append("<td align=\"right\" class=\"td_quantity\">");
                    mStr.Append("" + it.Number + "");
                    mStr.Append("</td>");
                    mStr.Append("<td align=\"right\" class=\"td_price\">" + it.SaleOff + " %</td>");
                    mStr.Append("<td align=\"right\" class=\"td_last\">");
                    mStr.Append("<div id=\"total_price_929\">" + String.Format("{0: 0,0}", (it.Price - ((it.Price / 100) * it.SaleOff))) + "  VNĐ </div>");
                    mStr.Append("</td>");
                    mStr.Append("</tr>");
                }
                mStr.Append("</table>");
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static int TinhTongGiaGioHang(List<Product> mList)
        {
            int x = 0;
            try
            {
                //Tao dnh sach san pham cho nhom m
                foreach (Product it in mList)
                {
                    x = (int)(x + (it.Price));
                }
                return x;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return 0;
            }
        }
        public static string TaoDanhSachHoTro(List<Support> mList)
        {
            StringBuilder mStr = new StringBuilder();

            try
            {
                List<Support> mList2 = (from p in mList
                                        where p.ID > 0
                                        orderby p.TypeID
                                        select p).ToList();
                //Tao dnh sach san pham cho nhom m
                foreach (Support it in mList)
                {
                    if (it.TypeID == 1)
                    {
                        mStr.Append("<div class=\"div_online\">");
                        mStr.Append("<a href=\"ymsgr:sendim?" + it.Nick + "\" style=\"text-decoration: none;\"><img alt=\"\" title=\"" + it.Name + "\" src=\"http://opi.yahoo.com/online?u=" + it.Nick + "&t=2\" /></a>");
                        mStr.Append("<p class=\"p_nameonline\">" + it.Name + " - " + it.Phone + "</p>");
                        mStr.Append("</div>");
                    }
                    else if (it.TypeID == 2)
                    {
                        mStr.Append("<div class=\"div_online\">");
                        mStr.Append("<a href=\"skype:" + it.Nick + "?chat\" style=\"text-decoration: none;\"><img alt=\"\" title=\"" + it.Name + "\" src=\"http://mystatus.skype.com/balloon/" + it.Nick + "\" /></a>");
                        mStr.Append("<p class=\"p_nameonline\">" + it.Name + " - " + it.Phone + "</p>");
                        mStr.Append("</div>");
                    }
                }
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }

        public static string TaoDanhSachHoaDon(List<ProductOrder> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">FullName</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Phone</th><th scope=\"col\" class=\"rgHeader\">Giá</th><th scope=\"col\" class=\"rgHeader\">Mô tả</th><th scope=\"col\" class=\"rgHeader\">Địa Chỉ</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (ProductOrder it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + it.Price + "</td>");
                        mStr.Append("<td>" + it.Detail + "</td>");
                        mStr.Append("<td>" + it.Address + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/ProductOrder/Detail?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        if (it.Status == 0)
                            mStr.Append("<a href=\"javascript:void(0);\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon.png\" /></a>");
                        else
                            mStr.Append("<a href=\"javascript:void(0);\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon2.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_Order_HienThiPopUp('Bạn có thực sự muốn xóa đơn hàng này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + String.Format("{0:0,0}", it.Price) + "</td>");
                        mStr.Append("<td>" + it.Detail + "</td>");
                        mStr.Append("<td>" + it.Address + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/ProductOrder/Detail?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        if (it.Status == 1)
                            mStr.Append("<a href=\"javascript:void(0);\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon.png\" /></a>");
                        else
                            mStr.Append("<a href=\"javascript:void(0);\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon2.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_Order_HienThiPopUp('Bạn có thực sự muốn xóa đơn hàng này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }

        public static string TaoDanhSachThongKeDongHang(List<ProductOrder> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">FullName</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Phone</th><th scope=\"col\" class=\"rgHeader\">Giá trị ĐH</th><th scope=\"col\" class=\"rgHeader\">Mô tả</th><th scope=\"col\" class=\"rgHeader\">Địa Chỉ</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (ProductOrder it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + String.Format("{0:0,0}",it.Price) + "</td>");
                        mStr.Append("<td>" + it.Detail + "</td>");
                        mStr.Append("<td>" + it.Address + "</td>");                        
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + String.Format("{0:0,0}", it.Price) + "</td>");
                        mStr.Append("<td>" + it.Detail + "</td>");
                        mStr.Append("<td>" + it.Address + "</td>");                        
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }

        public static string TaoDanhSachThongKeThang(List<ProductOrder> pList, int pPage, DateTime mDate1, DateTime mDate2)
        {
            StringBuilder mStr = new StringBuilder();
            try
            {
                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Ngày tháng</th><th scope=\"col\" class=\"rgHeader\">Số đơn đặt hàng</th><th scope=\"col\" class=\"rgHeader\">Số đơn giao</th><th scope=\"col\" class=\"rgHeader\">Giao một phần</th><th scope=\"col\" class=\"rgHeader\">Hủy</th>");
                mStr.Append("<tbody>");
                int listday = (mDate2 - mDate1).Days;
                for (int j = 0; j < listday; j++)
                {
                    var fromStartDate = new DateTime();
                    fromStartDate = mDate1.AddDays(j);
                    var toStartDate = new DateTime();
                    toStartDate = mDate1.AddDays(j).AddDays(1);
                    int dongiao = pList.Where(x => x.Date >= fromStartDate && x.Date <= toStartDate && x.Status == 1).ToList().Count;
                    int dongiao1phan = pList.Where(x => x.Date >= fromStartDate && x.Date <= toStartDate && x.Status == 2).ToList().Count;
                    int donhuy = pList.Where(x => x.Date >= fromStartDate && x.Date <= toStartDate && x.Status == -1).ToList().Count;
                    int dondat = pList.Where(x => x.Date >= fromStartDate && x.Date <= toStartDate && x.Status == 0).ToList().Count + dongiao + dongiao1phan + donhuy;
                    mStr.Append("<tr class=\"rgRow\">");
                    mStr.Append("<td>" + mDate1.AddDays(j).ToString("dd/MM/yyyy") + "</td>");
                    mStr.Append("<td>" + dondat + "</td>");
                    mStr.Append("<td>" + dongiao + "</td>");
                    mStr.Append("<td>" + dongiao1phan + "</td>");
                    mStr.Append("<td>" + donhuy + "</td>");
                    mStr.Append("</tr>");
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }

        public static string TaoDanhSachCacAdminAccount(List<Admin> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Tài khoản</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (Admin it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.UserName + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/AdminAccount/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_AdminAccount_HienThiPopUp('Bạn có thực sự muốn xóa tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.UserName + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/AdminAccount/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        mStr.Append("<a href=\"javascript:Admin_AdminAccount_HienThiPopUp('Bạn có thực sự muốn xóa  tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachCacAccount(List<Account> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Tài khoản</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (Account it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.UserName + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/Account/Detail?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        if (it.Status == true)
                            mStr.Append("<a href=\"javascript:Admin_Account_HienThiPopUpAn('Bạn có thực sự muốn khóa tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon.png\" /></a>");
                        else
                            mStr.Append("<a href=\"javascript:Admin_Account_HienThiPopUpHien('Bạn có thực sự muốn khóa tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon2.png\" /></a>");

                        mStr.Append("<a href=\"javascript:Admin_Account_HienThiPopUp('Bạn có thực sự muốn xóa tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.UserName + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/Account/Detail?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        if (it.Status == true)
                            mStr.Append("<a href=\"javascript:Admin_Account_HienThiPopUpAn('Bạn có thực sự muốn khóa tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon.png\" /></a>");
                        else
                            mStr.Append("<a href=\"javascript:Admin_Account_HienThiPopUpHien('Bạn có thực sự muốn kích hoạt lại tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon2.png\" /></a>");

                        mStr.Append("<a href=\"javascript:Admin_Account_HienThiPopUp('Bạn có thực sự muốn xóa  tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachCacMarket(List<Market> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {

                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">Tên</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Tài khoản</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (Market it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.UserName + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/Market/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        if (it.Status == true)
                            mStr.Append("<a href=\"javascript:Admin_Market_HienThiPopUpAn('Bạn có thực sự muốn khóa tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon.png\" /></a>");
                        else
                            mStr.Append("<a href=\"javascript:Admin_Market_HienThiPopUpHien('Bạn có thực sự muốn khóa tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon2.png\" /></a>");

                        mStr.Append("<a href=\"javascript:Admin_Market_HienThiPopUp('Bạn có thực sự muốn xóa tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.UserName + "</td>");
                        mStr.Append("<td><a class=\"green\" href=\"/Market/Edit?pId=" + it.ID + "\">");
                        mStr.Append("<img class=\"micon\" src=\"/Content/Images/Edit-icon.png\" /></a>");
                        if (it.Status == true)
                            mStr.Append("<a href=\"javascript:Admin_Market_HienThiPopUpAn('Bạn có thực sự muốn khóa tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon.png\" /></a>");
                        else
                            mStr.Append("<a href=\"javascript:Admin_Market_HienThiPopUpHien('Bạn có thực sự muốn kích hoạt lại tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"blue\"><img class=\"micon\" src=\"/Content/Images/hidenicon2.png\" /></a>");

                        mStr.Append("<a href=\"javascript:Admin_Market_HienThiPopUp('Bạn có thực sự muốn xóa  tài khoản này ?'," + it.ID + "," + pPage + ");\" class=\"btred\"><img class=\"micon\" src=\"/Content/Images/DeleteRed.png\" /></a>");
                        mStr.Append("</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachAnhSlideSanPham(List<ProductImage> mList)
        {
            StringBuilder mStr = new StringBuilder();

            try
            {
                foreach (ProductImage it in mList)
                {
                    mStr.Append("<div class=\"slidegalaxyittem\"  id=\"slidegalaxyittemid_" + it.ID + "\"><div class=\"slidegalaxyclosebutton\" onclick=\"javascript:CloseButton('slidegalaxyittemid_" + it.ID + "');\"></div><img style=\"width:100px;height:60px;margin-left:10px;\" src=\"" + it.Name + "\" /><input type=\"hidden\" value=\"" + it.Name + "\" name=\"imageslide\"/></div>");
                }
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachAnhAttributeSanPham(List<ProductAttribute> mList)
        {
            StringBuilder mStr = new StringBuilder();

            try
            {
                foreach (ProductAttribute it in mList)
                {
                    mStr.Append("<div class=\"slidepropertiesittem\"   id=\"slidepropertiesittemid_" + it.ID + "\"><div class=\"slidepropertiesclosebutton\" onclick=\"javascript:slidepropertiesCloseButton('slidepropertiesittemid_" + it.ID + "');\"></div><input value=\"" + it.Name + "\" placeholder=\"Tên thuộc tính\" style=\"min-width:40px;\" type=\"text\" name=\"pNamePro\" /><input value=\"" + it.Value + "\"  placeholder=\"Giá trị\"   style=\"min-width:40px;\"  type=\"text\" name=\"pValuePro\" /></div>");
                }
                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }






        static public string TaoDanhSachTinHot(List<News> pList)
        {
            StringBuilder Str = new StringBuilder();
            int i = 1;
            try
            {
                foreach (News it in pList)
                {
                    if (i == 1)
                    {
                        Str.Append("<div class=\"spl-item  item active\" data-href=\"/chi-tiet/" + it.ID + "/" + Ultility.LocDau(it.Title) + "\">");
                        Str.Append("<span class=\"spl-item-title\"><a href=\"/chi-tiet/" + it.ID + "/" + Ultility.LocDau(it.Title) + "\" title=\"" + it.Title + "\">" + it.Title + "</a></span><span class=\"spl-item-date\"> - " + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + " </span>");
                        Str.Append("</div>");
                    }
                    else
                    {
                        Str.Append("<div class=\"spl-item  item\" data-href=\"/chi-tiet/" + it.ID + "/" + Ultility.LocDau(it.Title) + "\">");
                        Str.Append("<span class=\"spl-item-title\"><a href=\"/chi-tiet/" + it.ID + "/" + Ultility.LocDau(it.Title) + "\" title=\"" + it.Title + "\">" + it.Title + "</a></span><span class=\"spl-item-date\"> - " + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + " </span>");
                        Str.Append("</div>");
                    }
                    i++;
                }
                return Str.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }

        }
        static public string TaoDanhSachTinPhoBien(List<News> pList)
        {
            StringBuilder Str = new StringBuilder();
            //int i = 1;
            try
            {

                Str.Append("<div id=\"itempb1\" class=\"item active\">");
                Str.Append("<div class=\"line\">");
                Str.Append(TaoDanhTinPhoBien(pList[0]));
                Str.Append("<div class=\"clr1\"></div>");
                Str.Append(TaoDanhTinPhoBien(pList[1]));
                Str.Append("<div class=\"clr1 clr2\"></div>");
                Str.Append(TaoDanhTinPhoBien(pList[2]));
                Str.Append("<div class=\"clr1 clr3\"></div>");
                Str.Append(TaoDanhTinPhoBien(pList[3]));
                Str.Append("<div class=\"clr1 clr2 clr4\"></div>");
                Str.Append(TaoDanhTinPhoBien(pList[4]));
                Str.Append("<div class=\"clr1 clr5\"></div>");
                Str.Append("</div>");
                Str.Append("<!--line-->");
                Str.Append("</div>");
                Str.Append("<!--end item-->");

                Str.Append("<div id=\"itempb2\"  class=\"item \">");
                Str.Append(" <div class=\"line\">");
                Str.Append(TaoDanhTinPhoBien(pList[5]));
                Str.Append("<div class=\"clr1\">");
                Str.Append("</div>");
                Str.Append(TaoDanhTinPhoBien(pList[6]));
                Str.Append("<div class=\"clr1 clr2\">");
                Str.Append("</div>");
                Str.Append(TaoDanhTinPhoBien(pList[7]));
                Str.Append("<div class=\"clr1 clr3\">");
                Str.Append("</div>");
                Str.Append(TaoDanhTinPhoBien(pList[8]));
                Str.Append("<div class=\"clr1 clr2 clr4\">");
                Str.Append("</div>");
                Str.Append(TaoDanhTinPhoBien(pList[9]));
                Str.Append("<div class=\"clr1 clr5\">");
                Str.Append("</div>");
                Str.Append("</div>");
                Str.Append("<!--line-->");
                Str.Append("</div>");
                return Str.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }

        }
        static public string TaoDanhTinPhoBien(News pNews)
        {
            StringBuilder Str2 = new StringBuilder();
            //int i = 1;
            try
            {
                Str2.Append("<div class=\"item-wrap style2\">");
                Str2.Append(" <div class=\"item-wrap-inner\">");
                Str2.Append("  <div class=\"item-image\">");
                Str2.Append(" <img src=\"" + pNews.Image + "\" alt=\"\">");
                Str2.Append("</div>");
                Str2.Append("<div class=\"item-info\">");
                Str2.Append(" <div class=\"item-title\">");
                Str2.Append("<a href=\"/chi-tiet/" + pNews.ID + "/" + Ultility.LocDau(pNews.Title) + "\" title=\"" + pNews.Title + "\">" + pNews.Title + "</a>");
                Str2.Append("</div>");
                Str2.Append("<div class=\"info-extend\">");
                Str2.Append("<span class=\"moduleItemDateCreated\">" + ConverterUlti.GetNgayDangByDateTime(pNews.Date.Value) + "</span>");
                Str2.Append(" </div>");
                Str2.Append("</div>");
                Str2.Append("</div>");
                Str2.Append("</div>");
                return Str2.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }

        }
        static public string TaoDanhSachTinCauDo(List<News> pList, string pName, int pType)
        {
            StringBuilder Str = new StringBuilder();
            //int i = 1;
            try
            {
                Str.Append("<div class=\"module  bg_title4 teen-news3\">");
                Str.Append("<h3 class=\"modtitle\">" + pName + "</h3>");
                Str.Append("<div class=\"modcontent clearfix\">");
                Str.Append("<div style=\"height: 324px;max-height: 324px;\" id=\"mgi_wrap_13303977301389762151\" class=\"mgi-wrap theme3 preset01-1 preset02-1 preset03-1 preset04-1 preset05-1\">");
                Str.Append("<div class=\"mgi-box\">");
                Str.Append("<div class=\"mgi-items column1\">");

                foreach (News it in pList)
                {
                    Str.Append("<div class=\"item-wrap \">");
                    Str.Append("<div class=\"item-title\">");
                    Str.Append("<a href=\"/chi-tiet/" + it.ID + "/" + Ultility.LocDau(it.Title) + "\">" + it.Title + "</a>");
                    Str.Append("</div>");
                    Str.Append("</div>");
                    Str.Append("<div class=\"clear\">");
                    Str.Append("</div>");
                }
                Str.Append("</div>");
                Str.Append("<div class=\"view-all\">");
                Str.Append("<a href=\"/chuyen-muc/" + pType + "-1/" + Ultility.LocDau(pName) + "\">Xem thêm</a>");
                Str.Append("</div>");
                Str.Append("</div>");
                Str.Append("<!--end mgi-box-->");
                Str.Append("<div class=\"clr1\">");
                Str.Append("</div>");
                Str.Append("</div>");
                Str.Append("<!--end mgi_wrap-->");
                Str.Append("</div>");
                Str.Append("</div>");
                return Str.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }

        }

        static public string TaoDanhSachTinBanner(List<Image> pList)
        {
            StringBuilder Str = new StringBuilder();
            //int i = 1;
            try
            {
                foreach (Image it in pList)
                {
                    Str.Append("<div data-src=\"" + it.LinkImage + "\" data-thumb=\"" + it.LinkImage + "\"><div class=\"camera_caption\">" + it.Name + "</div></div>");
                }
                return Str.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }

        }
        static public string TaoDanhSachTinTrangChu(DanhSachNhomTin pList)
        {
            StringBuilder Str = new StringBuilder();
            int i = 0;
            try
            {
                while (i < pList.List.Count)
                {
                    Str.Append("<div class=\"hangtin\">");
                    Str.Append(TaoDanhSachTinTrangChu2(pList.List[i]));
                    Str.Append(TaoDanhSachTinTrangChu3(pList.List[i + 1]));
                    Str.Append("</div>");
                    i = (i + 2);
                }
                return Str.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }
        }
        static public string TaoDanhSachTinTrangChu2(NhomTin pNhomTin)
        {
            StringBuilder Str = new StringBuilder();
            //int i = 0;
            try
            {
                if (pNhomTin != null)
                {
                    if (pNhomTin.NewsList.Count > 0)
                    {
                        Str.Append("<div id=\"position-h3\" class=\"span6 first\"    style=\"height:765px;overflow:hidden;\">");
                        Str.Append("<div class=\"module  event\">");
                        Str.Append("<div class=\"modcontent clearfix\">");
                        Str.Append("<div id=\"mgi_wrap_14571871691389762151\" class=\"mgi-wrap preset01-1 preset02-1 preset03-1 preset04-1 preset05-1\" style=\"height:715px;\">");
                        Str.Append("<div class=\"mgi-cat google-font\">");
                        Str.Append("<a href=\"/chuyen-muc/" + pNhomTin.ID + "-1/" + Ultility.LocDau(pNhomTin.GroupName) + "\"><h2 style=\"font-size: 30px;\">" + pNhomTin.GroupName + "<h2></a>");
                        Str.Append("</div>");
                        Str.Append("<div class=\"mgi-box\">");
                        Str.Append("<div class=\"item-wrap\">");
                        Str.Append("<div class=\"item-image\" style=\"width:100%;cursor: pointer;\">");
                        Str.Append("<a href=\"/chi-tiet/" + pNhomTin.NewsList[0].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[0].Title) + "\">");
                        Str.Append("<img src=\"" + pNhomTin.NewsList[0].Image + "\" alt=\"\">");
                        Str.Append("</a>");
                        Str.Append("</div>");
                        Str.Append("<div class=\"item-title\">");
                        Str.Append("<a href=\"/chi-tiet/" + pNhomTin.NewsList[0].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[0].Title) + "\">" + pNhomTin.NewsList[0].Title + "</a></div>");
                        Str.Append("<div class=\"info-extend\">");
                        Str.Append("<span class=\"published\">" + ConverterUlti.GetNgayDangByDateTime(pNhomTin.NewsList[0].Date.Value) + "</span> <span class=\"hits\">" + pNhomTin.NewsList[0].Views + " lượt xem</span>");
                        Str.Append("</div>");
                        Str.Append("<p class=\"item-desc\">" + Ultility.SubtractStringSummaryWithNumber(pNhomTin.NewsList[0].Summary, 30) + "</p>");
                        Str.Append("</div>");
                        Str.Append("<div class=\"title-link-wrap\">");
                        Str.Append("<ul class=\"other-links\">");
                        Str.Append("<li><a href=\"/chi-tiet/" + pNhomTin.NewsList[1].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[1].Title) + "\">" + pNhomTin.NewsList[1].Title + "</a></li>");
                        Str.Append("<li><a href=\"/chi-tiet/" + pNhomTin.NewsList[2].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[2].Title) + "\">" + pNhomTin.NewsList[2].Title + "</a></li>");
                        Str.Append("<li><a href=\"/chi-tiet/" + pNhomTin.NewsList[3].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[3].Title) + "\">" + pNhomTin.NewsList[3].Title + "</a></li>");
                        Str.Append("<li><a href=\"/chi-tiet/" + pNhomTin.NewsList[4].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[4].Title) + "\">" + pNhomTin.NewsList[4].Title + "</a></li>");
                        Str.Append("<li><a href=\"/chi-tiet/" + pNhomTin.NewsList[5].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[5].Title) + "\">" + pNhomTin.NewsList[5].Title + "</a></li>");
                        Str.Append("</ul>");
                        Str.Append("</div>");
                        Str.Append("</div>");
                        Str.Append("<!--end mgi-box-->");
                        Str.Append("<div class=\"clr1\">");
                        Str.Append("</div>");
                        Str.Append("</div>");
                        Str.Append("<!--end mgi_wrap-->");
                        Str.Append("</div>");
                        Str.Append("</div>");
                        Str.Append("</div>");
                    }
                }
                return Str.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }
        }
        static public string TaoDanhSachTinTrangChu3(NhomTin pNhomTin)
        {
            StringBuilder Str = new StringBuilder();
            //int i = 0;
            try
            {
                if (pNhomTin != null)
                {
                    if (pNhomTin.NewsList.Count > 0)
                    {
                        Str.Append("<div id=\"position-h3\" class=\"span6\"    style=\"height:765px;overflow:hidden;\">");
                        Str.Append("<div class=\"module  event\">");
                        Str.Append("<div class=\"modcontent clearfix\">");
                        Str.Append("<div id=\"mgi_wrap_14571871691389762151\" class=\"mgi-wrap preset01-1 preset02-1 preset03-1 preset04-1 preset05-1\" style=\"height:715px;\">");
                        Str.Append("<div class=\"mgi-cat google-font\">");
                        Str.Append("<a href=\"/chuyen-muc/" + pNhomTin.ID + "-1/" + Ultility.LocDau(pNhomTin.GroupName) + "\"><h2 style=\"font-size: 30px;\">" + pNhomTin.GroupName + "</h2></a>");
                        Str.Append("</div>");
                        Str.Append("<div class=\"mgi-box\">");
                        Str.Append("<div class=\"item-wrap\">");
                        Str.Append("<div class=\"item-image\" style=\"width:100%;cursor: pointer;\">");
                        Str.Append("<a href=\"/chi-tiet/" + pNhomTin.NewsList[0].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[0].Title) + "\">");
                        Str.Append("<img src=\"" + pNhomTin.NewsList[0].Image + "\" alt=\"\">");
                        Str.Append("</a>");
                        Str.Append("</div>");
                        Str.Append("<div class=\"item-title\">");
                        Str.Append("<a href=\"/chi-tiet/" + pNhomTin.NewsList[0].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[0].Title) + "\">" + pNhomTin.NewsList[0].Title + "</a></div>");
                        Str.Append("<div class=\"info-extend\">");
                        Str.Append("<span class=\"published\">" + ConverterUlti.GetNgayDangByDateTime(pNhomTin.NewsList[0].Date.Value) + "</span> <span class=\"hits\">" + pNhomTin.NewsList[0].Views + " lượt xem</span>");
                        Str.Append("</div>");
                        Str.Append("<p class=\"item-desc\">" + Ultility.SubtractStringSummaryWithNumber(pNhomTin.NewsList[0].Summary, 30) + "</p>");
                        Str.Append("</div>");
                        Str.Append("<div class=\"title-link-wrap\">");
                        Str.Append("<ul class=\"other-links\">");
                        Str.Append("<li><a href=\"/chi-tiet/" + pNhomTin.NewsList[1].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[1].Title) + "\">" + pNhomTin.NewsList[1].Title + "</a></li>");
                        Str.Append("<li><a href=\"/chi-tiet/" + pNhomTin.NewsList[2].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[2].Title) + "\">" + pNhomTin.NewsList[2].Title + "</a></li>");
                        Str.Append("<li><a href=\"/chi-tiet/" + pNhomTin.NewsList[3].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[3].Title) + "\">" + pNhomTin.NewsList[3].Title + "</a></li>");
                        Str.Append("<li><a href=\"/chi-tiet/" + pNhomTin.NewsList[4].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[4].Title) + "\">" + pNhomTin.NewsList[4].Title + "</a></li>");
                        Str.Append("<li><a href=\"/chi-tiet/" + pNhomTin.NewsList[5].ID + "/" + Ultility.LocDau(pNhomTin.NewsList[5].Title) + "\">" + pNhomTin.NewsList[5].Title + "</a></li>");
                        Str.Append("</ul>");
                        Str.Append("</div>");
                        Str.Append("</div>");
                        Str.Append("<!--end mgi-box-->");
                        Str.Append("<div class=\"clr1\">");
                        Str.Append("</div>");
                        Str.Append("</div>");
                        Str.Append("<!--end mgi_wrap-->");
                        Str.Append("</div>");
                        Str.Append("</div>");
                        Str.Append("</div>");
                    }
                }
                return Str.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }
        }
        static public string TaoDanhSachTinTheoNhom(List<News> pList)
        {
            StringBuilder Str = new StringBuilder();
            //int i = 1;
            try
            {
                foreach (News it in pList)
                {
                    Str.Append("<div class=\"itemContainer itemContainerLast\" style=\"width: 100.0%;\">");
                    Str.Append("<div class=\"catItemView groupPrimary\">");
                    Str.Append("<div class=\"catItemBody\">");
                    Str.Append("<div class=\"catItemImageBlock\">");
                    Str.Append("<span class=\"catItemImage\">");
                    Str.Append("<a href=\"/chi-tiet/" + it.ID + "/" + Ultility.LocDau(it.Title) + ".html\" title=\"" + it.Title + "\">");

                    Str.Append("<img src=\"" + it.Image + "\" alt=\"" + it.Title + "\">");
                    Str.Append("</a>");
                    Str.Append("</span>");
                    Str.Append("<div class=\"clr\"></div>");
                    Str.Append("</div>");
                    Str.Append("<div class=\"catItemMain\">");

                    Str.Append("<h3 class=\"catItemTitle\">");

                    Str.Append("<a href=\"/chi-tiet/" + it.ID + "/" + Ultility.LocDau(it.Title) + ".html\">" + it.Title + "</a>");

                    Str.Append("</h3>");
                    Str.Append("<div class=\"itemCreate\">");


                    Str.Append("<div class=\"catItemCategory\">");
                    Str.Append("<div class=\"iconcategory\"></div>");

                    Str.Append("</div>");


                    Str.Append("<i class=\"fa fa-clock-o\"></i>");
                    Str.Append("<span class=\"catItemDateCreated\">" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</span>");

                    Str.Append("<div class=\"catItemHitsBlock\">");
                    Str.Append("<span class=\"catItemHits\">");
                    Str.Append("<i class=\"fa fa-eye\"></i><b>" + it.Views + " lượt xem </b>");
                    Str.Append("</span>");
                    Str.Append("</div>");

                    Str.Append("<div class=\"catItemRatingBlock\">");
                    Str.Append("<div class=\"itemRatingForm\">");
                    Str.Append("<ul class=\"itemRatingList\">");
                    Str.Append("<li class=\"itemCurrentRating\" id=\"itemCurrentRating55\" style=\"width: 100%;\"></li>");
                    Str.Append("<li><a href=\"#\" data-id=\"55\" title=\"1 star out of 5\" class=\"one-star\">1</a></li>");
                    Str.Append("<li><a href=\"#\" data-id=\"55\" title=\"2 stars out of 5\" class=\"two-stars\">2</a></li>");
                    Str.Append("<li><a href=\"#\" data-id=\"55\" title=\"3 stars out of 5\" class=\"three-stars\">3</a></li>");
                    Str.Append("<li><a href=\"#\" data-id=\"55\" title=\"4 stars out of 5\" class=\"four-stars\">4</a></li>");
                    Str.Append("<li><a href=\"#\" data-id=\"55\" title=\"5 stars out of 5\" class=\"five-stars\">5</a></li>");
                    Str.Append("</ul>");
                    Str.Append("<div id=\"itemRatingLog55\" class=\"itemRatingLog\">(1 Vote)</div>");
                    Str.Append("<div class=\"clr\"></div>");
                    Str.Append("</div>");
                    Str.Append("<div class=\"clr\"></div>");
                    Str.Append("</div>");

                    Str.Append("</div>");
                    Str.Append("<div class=\"catItemIntroText\">");
                    Str.Append("<p>" + it.Summary + "</p>");
                    Str.Append("</div>");
                    Str.Append("</div>");


                    Str.Append("<div class=\"clr\"></div>");
                    Str.Append("<div class=\"clr\"></div>");
                    Str.Append("</div>");
                    Str.Append("<div class=\"clr\"></div>");
                    Str.Append("<div class=\"clr\"></div>");
                    Str.Append("<div class=\"clr\"></div>");
                    Str.Append("<div class=\"clr\"></div>");
                    Str.Append("</div>");
                    Str.Append("</div>");
                    Str.Append("<div class=\"clr\"></div>");
                }
                return Str.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }

        }
        static public string TaoDanhSachTinTheoNhomListDEMO(List<Product> pList)
        {
            StringBuilder Str = new StringBuilder();
            //int i = 1;
            try
            {
                foreach (Product it in pList)
                {
                    Str.Append("<div class=\"alola\"  style=\"width: 15.0%;display: inline-block; clear: both; overflow: hidden; margin-right: 30px; margin-top: 50px;\">");

                    Str.Append("<div>");
                    Str.Append("<a target=\"_blank\" href=\"" + it.Summary + "\" title=\"" + it.Name + "\">");
                    Str.Append("<img src=\"" + it.Image + "\" alt=\"" + it.Name + "\">");
                    Str.Append("</a>");
                    Str.Append("</div>");

                    Str.Append("<div style=\"width: 100%; height: 60px; text-align: center; font-size: 12px; padding: 0px;margin-top:0px;\">");
                    Str.Append("<a target=\"_blank\"  href=\"" + it.Summary + "\" title=\"" + it.Name + "\">" + it.Name + "</a>");
                    Str.Append("</div>");

                    Str.Append("</div>");
                }
                return Str.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }

        }
        static public string TaoDanhSachNhomTinLienQuan(List<News> pList, int pId)
        {
            StringBuilder Str = new StringBuilder();
            //int i = 1;
            try
            {
                foreach (News it in pList)
                {
                    if (it.ID != pId)
                    {
                        if (it.ID % 2 == 0)
                            Str.Append("<li class=\"even\"><span>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</span> :<a  href=\"/chi-tiet/" + it.ID + "/" + Ultility.LocDau(it.Title) + ".html\">   " + it.Title + "</a> </li>");
                        else
                            Str.Append("<li  class=\"odd\"><span>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</span> :<a  href=\"/chi-tiet/" + it.ID + "/" + Ultility.LocDau(it.Title) + ".html\">    " + it.Title + "</a> </li>");
                    }
                }
                return Str.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }

        }
        static public string TaoDanhSachNhomTinGocTrai(List<News> pList, int pId)
        {
            StringBuilder Str = new StringBuilder();
            //int i = 0;
            try
            {
                if (pList[0] != null)
                {
                    Str.Append("<div class=\"left new2 w100pt\">");
                    Str.Append("<a href=\"/chi-tiet/" + pList[0].ID + "/" + Ultility.LocDau(pList[0].Title) + "\">");
                    Str.Append("<div class=\"imgNew170 second-effect\">");
                    Str.Append("<div class=\"v308textheadimg\">");
                    Str.Append("<img style=\"\" src=\"" + pList[0].Image + "\">");
                    Str.Append("</div>");
                    Str.Append("<!-- img:width-heighht(230x147) -->");
                    Str.Append("<div class=\"mask\">");
                    Str.Append("<div class=\"info\">");
                    Str.Append("Xem chi tiết");
                    Str.Append("</div>");
                    Str.Append("</div>");
                    Str.Append("</div>");
                    Str.Append("</a>");
                    Str.Append("<div class=\"v308textheadouter left w100pt bgEF pdt10\">");
                    Str.Append("<a class=\"v308texthead\" href=\"/chi-tiet/" + pList[0].ID + "/" + Ultility.LocDau(pList[0].Title) + "\">" + pList[0].Title + "</a>");
                    Str.Append("</div>");
                    Str.Append("</div>");

                    Str.Append("<div class=\"bgEF w100pt left\">");
                    Str.Append("<div class=\"w100pt left\">");
                    Str.Append("<ul class=\"left newkhac2 mT10 hover w140 mL10 li_line\">");
                }
                if (pList.Count > 1)
                {
                    Str.Append("<li><a href=\"/chi-tiet/" + pList[1].ID + "/" + Ultility.LocDau(pList[1].Title) + "\" class=\"fz14\">");
                    Str.Append("1. " + pList[1].Title + "</a></li>");
                }
                if (pList.Count > 2)
                {
                    Str.Append("<li><a href=\"/chi-tiet/" + pList[2].ID + "/" + Ultility.LocDau(pList[2].Title) + "\" class=\"fz14\">");
                    Str.Append("2. " + pList[2].Title + "</a></li>");
                }
                if (pList.Count > 3)
                {
                    Str.Append("<li><a href=\"/chi-tiet/" + pList[3].ID + "/" + Ultility.LocDau(pList[3].Title) + "\" class=\"fz14\">");
                    Str.Append("3. " + pList[3].Title + "</a></li>");
                }
                Str.Append("</ul>");
                Str.Append("</div>");
                Str.Append("</div>");
                return Str.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }

        }

        static public string createShopCart(ShopCart pShopCart)
        {
            StringBuilder Str = new StringBuilder();
            try
            {
                foreach (Product it in pShopCart.List)
                {
                    Str.Append("<div class=\"t6 is-row-product-pop\"><a href=\"/" + Ultility.LocDau(it.Name) + "_d" + it.ID + ".html\" class=\"t7\">");
                    Str.Append("<img src=\"" + it.Image + "\" class=\"mCS_img_loaded\">");
                    Str.Append("</a>");
                    Str.Append("<div class=\"t8\"><a href=\"/" + Ultility.LocDau(it.Name) + "_d" + it.ID + ".html\">" + it.Name + "</a><span><span class=\"vl is-money\">" + String.Format("{0: 0,0}", (it.Price - ((it.Price / 100) * it.SaleOff))) + "</span><span class=\"t8a\">đ</span></span></div>");
                    Str.Append("<div class=\"t81\">");
                    //Str.Append("<span class=\"t82 fa fa-minus is-change-quanlity-pop\"><span class=\"fa fa-angle-down\"></span></span>");
                    //Str.Append("<input type=\"text\" class=\"t83\" value=\"1\" min=\"1\">");
                    //Str.Append("<span class=\"t82 fa fa-plus is-change-quanlity-pop\"><span class=\"fa fa-angle-up\"></span></span>");
                    Str.Append("</div>");
                    Str.Append("<div class=\"t84\"><span class=\"vl is-money is-money-ajax wait\">x " + it.Number + "</span><span class=\"t84a\"></span></div>");
                    Str.Append("<div class=\"t85 is-remove-product-pop\"><span class=\"fa fa-close\"></span></div>");
                    Str.Append("</div>");
                }
                return Str.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "<dx></dx>";
            }

        }

        public static string TaoDanhSachKHMuaMotLan(List<ProductOrder> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {
                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã</th><th scope=\"col\" class=\"rgHeader\">FullName</th><th scope=\"col\" class=\"rgHeader\">Ngày đăng</th><th scope=\"col\" class=\"rgHeader\">Phone</th><th scope=\"col\" class=\"rgHeader\">Giá trị ĐH</th><th scope=\"col\" class=\"rgHeader\">Mô tả</th><th scope=\"col\" class=\"rgHeader\">Địa Chỉ</th><th scope=\"col\" class=\"rgHeader\">Action</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (ProductOrder it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + String.Format("{0:0,0}", it.Price) + "</td>");
                        mStr.Append("<td>" + it.Detail + "</td>");
                        mStr.Append("<td>" + it.Address + "</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.ID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + String.Format("{0:0,0}", it.Price) + "</td>");
                        mStr.Append("<td>" + it.Detail + "</td>");
                        mStr.Append("<td>" + it.Address + "</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachKHVip(List<ProductOrder> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {
                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã KH</th><th scope=\"col\" class=\"rgHeader\">FullName</th><th scope=\"col\" class=\"rgHeader\">Phone</th><th scope=\"col\" class=\"rgHeader\">Địa Chỉ</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (ProductOrder it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.AccountID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + it.Address + "</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.AccountID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + it.Address + "</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachKHCu(List<ProductOrder> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {
                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã KH</th><th scope=\"col\" class=\"rgHeader\">FullName</th><th scope=\"col\" class=\"rgHeader\">Phone</th><th scope=\"col\" class=\"rgHeader\">Địa Chỉ</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (ProductOrder it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.AccountID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + it.Address + "</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.AccountID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + it.Address + "</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
        public static string TaoDanhSachKHMoi(List<ProductOrder> pList, int pPage)
        {
            StringBuilder mStr = new StringBuilder();
            int i = 1;
            try
            {
                mStr.Append("");
                mStr.Append("<table class=\"rgMasterTable\" style=\"width: 100%; table-layout: auto; empty-cells: show;\">");
                mStr.Append("<colgroup><col><col><col><col><col><col><col><col></colgroup>");
                mStr.Append("<thead><tr><th scope=\"col\" class=\"rgHeader\">Mã KH</th><th scope=\"col\" class=\"rgHeader\">FullName</th><th scope=\"col\" class=\"rgHeader\">Phone</th><th scope=\"col\" class=\"rgHeader\">Địa Chỉ</th></tr></thead>");
                mStr.Append("<tbody>");
                foreach (ProductOrder it in pList)
                {
                    if ((i % 2) == 0)
                    {
                        mStr.Append("<tr class=\"rgAltRow\">");
                        mStr.Append("<td>" + it.AccountID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + it.Address + "</td>");
                        mStr.Append("</tr>");
                    }
                    else
                    {
                        mStr.Append("<tr class=\"rgRow\">");
                        mStr.Append("<td>" + it.AccountID + "</td>");
                        mStr.Append("<td>" + it.FullName + "</td>");
                        mStr.Append("<td>" + it.Phone + "</td>");
                        mStr.Append("<td>" + it.Address + "</td>");
                        mStr.Append("</tr>");
                    }
                    i++;
                }
                mStr.Append("</tbody>");
                mStr.Append("</table>");

                return mStr.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }
    }
}