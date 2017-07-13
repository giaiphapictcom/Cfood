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
        //Builds URL by finding the best matching route that corresponds to the current URL,
        //with given parameters added or replaced.
        public static MvcHtmlString CategoryFilterUrl(this UrlHelper helper,RouteValueDictionary currentRouteData,string filterBrandValue, string filterParamName="filter")
        {
            
            //get the route data for the current URL e.g. /Research/InvestmentModelling/RiskComparison
            //this is needed because unlike UrlHelper.Action, UrlHelper.RouteUrl sets includeImplicitMvcValues to false
            //which causes it to ignore current ViewContext.RouteData.Values
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

                var newFilterBrandValue = rd[filterParamName] + filterBrandValue;
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