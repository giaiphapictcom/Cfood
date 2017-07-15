using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace V308CMS.Helpers
{
    public static class UrlHelperExtensions
    {
        public enum  BrandFilterAction
        {
            AppendFilter = 1,
            RemoveFilter = 2
        }
      
        public static MvcHtmlString CategoryFilterUrl(this UrlHelper helper,RouteValueDictionary currentRouteData, byte filterBrandType, string filterBrandValue, string filterParamName="filter")
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
                var listFilterToken =
                       (rd[filterParamName] + filterBrandValue).Split(new[] { "|" },
                           StringSplitOptions.RemoveEmptyEntries).ToList();
                var newFilterBrandValue = "|" + listFilterToken[0];
                for (int i = 1; i < listFilterToken.Count; i++)
                {
                    newFilterBrandValue = newFilterBrandValue + listFilterToken[i].Replace(filterBrandType + "_", ",");
                }
                newFilterBrandValue = newFilterBrandValue + "|";
                rd.Remove(filterParamName);
                rd[filterParamName] = newFilterBrandValue;



            }
            else
            {
                rd[filterParamName] = filterBrandValue;
            }
            var url = helper.RouteUrl(rd);
            return new MvcHtmlString(url);
        }
}
}