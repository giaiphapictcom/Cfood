using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace V308CMS.Common
{
    public class DataHelper
    {
        public static IEnumerable<SelectListItem> ListDay
        {
            
            get
            {
                var listMonth = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Ngày", Value = "0", Selected = true}

                };
                for (int i = 1; i <= 31; i++)
                {
                    listMonth.Add( new SelectListItem { Text = i.ToString(), Value = i.ToString() });

                }
                return listMonth;


            }
        }
        public static IEnumerable<SelectListItem> ListMonth
        {

            get
            {
                var listMonth = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Tháng", Value = "0", Selected = true}

                };
                for (int i = 1; i <= 12; i++)
                {
                    listMonth.Add(new SelectListItem { Text = i < 10 ? "0" + i : i.ToString(), Value = i.ToString() });

                }
                return listMonth;


            }
        }
        public static IEnumerable<SelectListItem> ListYear
        {

            get
            {
                var listYear = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Năm", Value = "0", Selected = true}

                };
                for (int i = DateTime.Now.Year ; i >= 1897; i--)
                {
                    listYear.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

                }
                return listYear;


            }
        }
    }
}