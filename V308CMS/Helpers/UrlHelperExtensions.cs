using System.Web.Mvc;
using System.Web.Routing;

namespace V308CMS.Helpers
{
    public static class UrlHelperExtensions
    {
        public enum  FilterAction
        {
            AppendFilter = 1,                        
            RemoveFilter = 2
        }
        public static string CategoryFilterUrl(this UrlHelper helper,RouteValueDictionary currentRouteData,
            string filterValueToken,
            byte filterAction = (byte)FilterAction.AppendFilter,
            string filterParamName="filter")
        {
            var rd = new RouteValueDictionary(currentRouteData);
            var qs = helper.RequestContext.HttpContext.Request.QueryString;
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
                   
                    if (!currFilterValue.Contains(filterValueToken))
                    {                        
                        newFilterValueResult = filterValueToken;
                    }                                     
                }
                else
                {
                    newFilterValueResult = currFilterValue.Replace(filterValueToken, "");
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