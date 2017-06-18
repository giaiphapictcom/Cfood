using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Respository;

namespace V308CMS.Admin.Middlewares
{
    public class CheckPermissionMiddleware:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentUser = filterContext.HttpContext.User as CustomPrincipal;
            if(currentUser == null) return;
            var controller = filterContext.RouteData.Values["controller"].ToString();
            //Bo qua cac controller co CheckGroupPermissionAttribute va HasCheckPermission = false
            var checkGroupPermission =(CheckGroupPermissionAttribute)filterContext.Controller.GetType().GetCustomAttributes(typeof(CheckGroupPermissionAttribute),false)
                .FirstOrDefault();           
            if (checkGroupPermission != null && checkGroupPermission.HasCheckPermission == false) return;
            //Bo qua cac action co SkipCheckPermissionAttribute va SkipCheckPermission = true
            var skipCheckPermission =(SkipCheckPermissionAttribute)filterContext.ActionDescriptor.GetCustomAttributes(typeof (SkipCheckPermissionAttribute), true).FirstOrDefault();
            if (skipCheckPermission != null && skipCheckPermission.SkipCheckPermission) return;
            //Bo qua cac action khong co CheckPermissionAttribute
            var checkPermission =(CheckPermissionAttribute)filterContext.ActionDescriptor.GetCustomAttributes(typeof(CheckPermissionAttribute), true).FirstOrDefault();
            if (checkPermission == null) return;
            var permissionService = new  PermissionRespository();
            var permission = permissionService.GetPermissionValueByGroupAndRole(controller+ "Permission", currentUser.RoleId);
            if(((int)Math.Pow(2, checkPermission.Index) & permission) == 0)
            {
                filterContext.Result = new RedirectToRouteResult(new
                     RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}