using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;
using V308CMS.Data.Enum;

namespace V308CMS.Helpers
{
    public static class UrlHelperExtensions
    {
        public enum  FilterAction
        {
            AppendFilter = 1,
            RemoveFilter = 2
        }

        private static string RebuildFilterToken(string currFilterValue, string filterValueToken, byte filterType)
        {
            
            var filterTypeValue = Regex.Match(currFilterValue, $@"\|?{filterType}_\d+(\,?\d+\,?)+\|?");
            if (!filterTypeValue.Success)
            {
                return "";
            }
            var result = "";
            var listFilterToken = filterTypeValue.Value.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();
         
            var listTokenCategory = listFilterToken.Where(item => item.Contains(filterType + "_")).ToList();

         
            for (int i = 0; i < listTokenCategory.Count; i++)
            {

                result = result + listTokenCategory[i].Replace(filterType + "_", ",");
            }
            result = (result == "|" ? "" : result) + "|" + filterType + "_" + result + "|";
            return result;
        }
        public static string CategoryFilterUrl(this UrlHelper helper,RouteValueDictionary currentRouteData, byte filterType, string filterValue,
            string filterValueToken, byte filterAction = (byte)FilterAction.AppendFilter, string filterParamName="filter")
        {
            var rd = new RouteValueDictionary(currentRouteData);

            //get the current query string e.g. ?BucketID=17371&amp;compareTo=123
            var qs = helper.RequestContext.HttpContext.Request.QueryString;

            //add query string parameters to the route value dictionary
            foreach (string param in qs)
            {              
                if (!string.IsNullOrEmpty(qs[param]))
                {
                    rd[param] = qs[param];
                }               
            }
         
            if (rd.ContainsKey(filterParamName))
            {
                var currFilterValue = rd[filterParamName].ToString();
                var newFilterValueResult = "";
                if (filterAction == (byte) FilterAction.AppendFilter)
                {
                    if (currFilterValue.Contains(filterType + "_"))
                    {
                        newFilterValueResult = RebuildFilterToken(currFilterValue, filterValueToken, filterType);                        
                    }
                    else
                    {
                        newFilterValueResult = currFilterValue + filterValueToken;
                    }
                   
                }
                else
                {
                    newFilterValueResult = currFilterValue.Contains(",") ? currFilterValue.Replace("," + filterValue, "").Replace(filterValue + ",", "") : currFilterValue.Replace(filterType + "_" + filterValue, "");

                    if (newFilterValueResult == "||")
                    {
                        newFilterValueResult = "";
                    }
                }
                rd.Remove(filterParamName);
                rd[filterParamName] = newFilterValueResult;
            }
            else
            {
                rd[filterParamName] = filterValueToken;
            }
            return  helper.RouteUrl(rd);
         
        }
}
}