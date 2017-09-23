using System.Web.Mvc;
using V308CMS.Data;

namespace V308CMS.Filters
{
    public class CategoryUpdateView: ActionFilterAttribute
    {
        private readonly string _idParamName;
        public CategoryUpdateView(string idParamName = "id")
        {
            _idParamName = idParamName;

        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = filterContext.ActionParameters[_idParamName] != null ? filterContext.ActionParameters[_idParamName].ToString() : "";
            if (!string.IsNullOrEmpty(id))
            {
                int cateId;
                int.TryParse(id, out cateId);
                if (cateId > 0)
                {
                    new ProductTypeRepository().IncrementView(cateId);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}