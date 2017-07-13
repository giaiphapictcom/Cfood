using System;
using System.Web.Mvc;
using V308CMS.Data;

namespace V308CMS.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ProductUpdateView : ActionFilterAttribute
    {
        private readonly string _idParamName;
        public ProductUpdateView(string idParamName= "pId")
        {
            _idParamName = idParamName;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = filterContext.ActionParameters[_idParamName]  != null? filterContext.ActionParameters[_idParamName].ToString() : "";
            var controller = !string.IsNullOrWhiteSpace(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName)? filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower() :"";
            if (controller =="home" && !string.IsNullOrEmpty(id))
            {
                int productId;
                int.TryParse(id, out productId);
                if (productId > 0)
                {
                    new ProductRepository().IncrementView(productId);
                }                          
            }
            base.OnActionExecuting(filterContext);
        }
    }
}