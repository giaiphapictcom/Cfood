using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace V308CMS.Common
{
    
    public class DataHelper
    {
        
        public static IEnumerable<SelectListItem> ListHour
        {

            get
            {
                var listHour = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Chọn giờ giao hàng", Value = "0", Selected = true}

                };
                for (int i = 0; i <= 24; i++)
                {
                    listHour.Add(new SelectListItem { Text = i + "H00", Value = i.ToString() });

                }
                return listHour;
            }
        }
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
                    listMonth.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

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
                for (int i = DateTime.Now.Year; i >= 1897; i--)
                {
                    listYear.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

                }
                return listYear;


            }
        }

        public static List<SelectListItem> ListEnumTypeSepecial<T>()
        {
            var dictionary = (from object item in Enum.GetValues(typeof (T))
                              let fi = item.GetType().GetField(item.ToString())
                              let attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof (DescriptionAttribute), false)
                              select (attributes.Length > 0) ? 
                                attributes[0].Description : 
                                item.ToString()).ToDictionary(description => description.ToUnsign().ToLower());
            return new SelectList(dictionary, "Key", "Value", 1).ToList();
        }
        public static List<SelectListItem> ListEnumType<T>(object selectedValue = null)
        {
            var dictionary = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var fi = item.GetType().GetField(item.ToString());
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = (attributes.Length > 0) ? attributes[0].Description : item.ToString();
                dictionary.Add((int)item, description);
            }
            return new SelectList(dictionary, "Key", "Value",selectedValue).ToList();
        }
        public static string GetStringEnum<T>(int obj) where T : struct
        {
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                if ((int)item != obj) continue;
                var fi = item.GetType().GetField(item.ToString());
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = (attributes.Length > 0) ? attributes[0].Description : item.ToString();
                return description;
            }
            return string.Empty;
        }
    }
}