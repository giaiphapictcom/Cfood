using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;

namespace V308CMS.Admin.Helpers {
    public class ControllerItem
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Value { get; set; }
    }

    public class ActionItem
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Value { get; set; }
    }
    public class ControllerActionItem
    {
        public ControllerItem ControllerItem { get; set; }
        public List<ActionItem> ListAction { get; set; }

    }
    public class ControllerHelper
    {
        public static List<ControllerActionItem> GetListControllerWithAction(string actionPrefix ="On")
        {
            var result = new List<ControllerActionItem>();
            if (ListControllers != null && ListControllers.Count > 0)
            {
                foreach (var controller in ListControllers)
                {                    
                    var checkGroupPermission = controller.GetCustomAttributes<CheckGroupPermissionAttribute>()                      
                       .FirstOrDefault();
                    if (checkGroupPermission != null && checkGroupPermission.HasCheckPermission)
                    {
                        var controllerItem = new ControllerItem
                        {
                            Name = checkGroupPermission.Description ?? controller.Name,
                            Code = controller.Name.Replace("Controller", "")
                        };
                        var listAction = ListControllers
                        .Where(item => item.Name == controller.Name)
                        .SelectMany(
                         item =>
                         item.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                         )
                        .ToList();
                        var newListAction = new List<ActionItem>();
                        int sumAction = 0;
                        if (listAction.Count > 0)
                        {                           
                            foreach (var action in listAction)
                            {
                                var checkPermissionAttr = action.GetCustomAttributes<CheckPermissionAttribute>()                           
                               .FirstOrDefault();
                                var actionNameAttr = action.GetCustomAttributes<ActionNameAttribute>()
                             .FirstOrDefault();
                                var isPermissionExist =
                                    newListAction.FirstOrDefault(
                                        item =>
                                        (actionNameAttr != null && actionNameAttr.Name == item.Name)||
                                        (item.Name == action.Name) || 
                                        (actionPrefix + item.Code == action.Name)                                     
                                        );
                                  

                                if (checkPermissionAttr != null && isPermissionExist == null)
                                {
                                    var value = (int)Math.Pow(2, checkPermissionAttr.Index);
                                    newListAction.Add(new ActionItem
                                    {
                                        Name = checkPermissionAttr.Name ?? action.Name,
                                        Code = action.Name,
                                        Value = value
                                    });
                                    sumAction += value;                                   
                                }

                            }
                            controllerItem.Value = sumAction;
                        }

                        result.Add(new ControllerActionItem
                        {
                            ControllerItem = controllerItem,
                            ListAction = newListAction
                        });
                    }
                }
                
            }
            return result;
        }
       
        private static readonly List<Type> ListControllers = Assembly
            .GetAssembly(typeof(MvcApplication))
            .GetTypes()
            .Where(type => typeof(Controller)
            .IsAssignableFrom(type))
            .ToList();

        public static int GetActionIndex(string controller, string action)
        {
            var fullControllerName = (controller + "controller").ToLower();
            var listAction = ListControllers
                .Where(item => item.Name.ToLower() == fullControllerName)
                .SelectMany(
                    item => item.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
               .ToList();
            if (listAction.Count > 0)
            {
                var actionCheck = listAction.FirstOrDefault(actionMethod => actionMethod.Name == action);
                if (actionCheck != null)
                {
                    var checkPermissionAttr = actionCheck.GetCustomAttributes<CheckPermissionAttribute>().FirstOrDefault();
                    if (checkPermissionAttr == null)
                    {
                        return -1;
                    }
                    return checkPermissionAttr.Index;
                }
                return -1;
            }
            return -1;
        }

        public static List<SelectListItem> GetListActionByController(string controller, string prefix = "On")
        {
            var fullControllerName = (controller + "controller").ToLower();
            var listAction = ListControllers
                .Where(item => item.Name.ToLower() == fullControllerName)
                .SelectMany(
                    item => item.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))             
               .ToList();
            var dictionary = new Dictionary<string, string>();
            if (listAction.Count > 0)
            {
                foreach (var action in listAction)
                {
                    var checkPermissionAttr = action.GetCustomAttributes<CheckPermissionAttribute>()                 
                   .FirstOrDefault();
                    if (action.Name.Contains(prefix) == false && checkPermissionAttr != null)
                    {
                        dictionary.Add(action.Name.ToLower(), checkPermissionAttr.Name ?? action.Name);
                    }
                 
                }
            }
            return new SelectList(dictionary, "Key", "Value", 1).ToList();

        }

        public static List<SelectListItem> GetListController()
        {
          
            var dictionary = new Dictionary<string, string>();
            if (ListControllers.Count>0)
            {
                foreach (var controller in ListControllers)
                {
                    var checkGroupPermission = controller.GetCustomAttributes<CheckGroupPermissionAttribute>()
                      .FirstOrDefault();
                    if (checkGroupPermission != null && checkGroupPermission.HasCheckPermission)
                    {
                        dictionary.Add(
                            controller.Name.Replace("Controller", "").ToLower(),
                            checkGroupPermission.Description ?? controller.Name);
                    }
                }              
            }
            return new SelectList(dictionary, "Key", "Value", 1).ToList();
        }
    }
}