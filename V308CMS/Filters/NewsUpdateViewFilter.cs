using System;
using System.Web.Mvc;
using V308CMS.Data;

namespace V308CMS.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NewsUpdateView:ActionFilterAttribute
    {
        private readonly string _idParamName;
        public NewsUpdateView(string idParamName="id")
        {
            _idParamName = idParamName;

        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = filterContext.ActionParameters[_idParamName] != null?  filterContext.ActionParameters[_idParamName].ToString() : "";
            if (!string.IsNullOrEmpty(id))
            {
                int newsId;
                int.TryParse(id, out newsId);
                if (newsId > 0)
                {                 
                    new NewsRepository().IncrementView(newsId);
                }

              
            }
            base.OnActionExecuting(filterContext);
        }
    }
}